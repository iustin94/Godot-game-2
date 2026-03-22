using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Creatures.Components;
using DungeonKeeper.Creatures.Definitions;
using DungeonKeeper.Dungeon.Map;
using DungeonKeeper.Dungeon.Placement;
using DungeonKeeper.Dungeon.Rooms;
using DungeonKeeper.GameState;
using DungeonKeeper.Scripts.Presenters;
using DungeonKeeper.Scripts.Rendering;
using Godot;

namespace DungeonKeeper.Scripts.Input;

public partial class InputHandler : Node3D
{
    public event Action<EntityId>? CreatureClicked;

    private GameSession? _session;
    private GodotMapPresenter? _mapPresenter;
    private RoomDefinitionRegistry? _roomRegistry;
    private RoomPlacementValidator _placementValidator = new();

    // Room building state
    private RoomType? _selectedRoomType;
    private RoomDefinition? _selectedRoomDef;

    public void Initialize(GameSession session, GodotMapPresenter mapPresenter)
    {
        _session = session;
        _mapPresenter = mapPresenter;
    }

    public void SetRoomRegistry(RoomDefinitionRegistry registry)
    {
        _roomRegistry = registry;
    }

    public void SelectRoom(RoomType? roomType)
    {
        if (roomType == null || _roomRegistry == null)
        {
            _selectedRoomType = null;
            _selectedRoomDef = null;
            return;
        }

        _selectedRoomType = roomType;
        _selectedRoomDef = _roomRegistry.Get(roomType.Value);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (_session == null) return;

        if (@event is InputEventMouseButton mb && mb.Pressed)
        {
            var tileCoord = RaycastToTile(mb.Position);
            if (tileCoord == null) return;

            if (mb.ButtonIndex == MouseButton.Left)
                HandleLeftClick(tileCoord.Value);
            else if (mb.ButtonIndex == MouseButton.Right)
                HandleRightClick(tileCoord.Value);
        }
    }

    private void HandleLeftClick(TileCoordinate coord)
    {
        if (_selectedRoomType != null)
        {
            TryPlaceRoomTile(coord);
            return;
        }

        // Check for creature at this tile
        var creatureId = FindCreatureAtTile(coord);
        if (creatureId != null)
        {
            CreatureClicked?.Invoke(creatureId.Value);
            return;
        }

        var tile = _session!.Map.GetTile(coord);
        if (tile == null) return;

        if (_session.Map.IsDiggable(coord) && !tile.IsMarkedForDigging)
        {
            tile.IsMarkedForDigging = true;
            _mapPresenter?.OnTileMarkedForDigging(coord);
            GD.Print($"Marked tile ({coord.X}, {coord.Y}) for digging");
        }
    }

    private void HandleRightClick(TileCoordinate coord)
    {
        var tile = _session!.Map.GetTile(coord);
        if (tile == null) return;

        if (tile.IsMarkedForDigging)
        {
            tile.IsMarkedForDigging = false;
            _mapPresenter?.OnTileUnmarked(coord);
            GD.Print($"Unmarked tile ({coord.X}, {coord.Y})");
        }
    }

    private void TryPlaceRoomTile(TileCoordinate coord)
    {
        if (_session == null || _selectedRoomDef == null || _selectedRoomType == null) return;
        if (_session.Players.Count == 0) return;

        var player = _session.Players[0];
        var dungeon = player.Dungeon;

        // Check placement validity
        if (!_placementValidator.CanPlace(_session.Map, coord, player.Id))
        {
            GD.Print($"Cannot place room at ({coord.X}, {coord.Y})");
            return;
        }

        // Check gold cost
        int cost = _selectedRoomDef.GoldCostPerTile;
        if (!dungeon.Gold.CanAfford(cost))
        {
            GD.Print($"Not enough gold! Need {cost}, have {dungeon.Gold.Current}");
            return;
        }

        // Check max per dungeon
        if (_selectedRoomDef.MaxPerDungeon.HasValue)
        {
            int existingCount = 0;
            foreach (var room in dungeon.OwnedRooms)
            {
                if (room.Type == _selectedRoomType.Value)
                    existingCount++;
            }
            if (existingCount >= _selectedRoomDef.MaxPerDungeon.Value)
            {
                GD.Print($"Maximum {_selectedRoomDef.Name} rooms reached ({_selectedRoomDef.MaxPerDungeon.Value})");
                return;
            }
        }

        // Deduct gold
        dungeon.Gold.TrySpend(cost);

        // Check if we can expand an adjacent room of the same type
        var adjacentRoom = FindAdjacentRoom(coord, _selectedRoomType.Value, player.Id);

        if (adjacentRoom != null)
        {
            // Expand existing room
            var tile = _session.Map.GetTile(coord)!;
            tile.Type = TileType.Room;
            tile.RoomType = _selectedRoomType;
            tile.RoomInstanceId = adjacentRoom.Id;
            tile.OwnerId = player.Id;
            adjacentRoom.Tiles.Add(coord);

            // Update map visuals
            _mapPresenter?.OnTileDug(coord); // Flatten the tile visually

            // Update room visuals
            var roomPresenter = _session.PresentationFactory.CreateRoomPresenter(adjacentRoom.Id, _selectedRoomDef.AssetId);
            roomPresenter.OnRoomExpanded(adjacentRoom.Id, new[] { coord });

            GD.Print($"Expanded {_selectedRoomDef.Name} at ({coord.X}, {coord.Y}) — now {adjacentRoom.TileCount} tiles");
        }
        else
        {
            // Create new room instance
            var roomId = EntityId.New();
            var tile = _session.Map.GetTile(coord)!;
            tile.Type = TileType.Room;
            tile.RoomType = _selectedRoomType;
            tile.RoomInstanceId = roomId;
            tile.OwnerId = player.Id;

            var room = new RoomInstance
            {
                Id = roomId,
                Type = _selectedRoomType.Value,
                OwnerId = player.Id,
                MinimumSize = _selectedRoomDef.MinimumSize,
                Health = 1000,
                Capacity = 4
            };
            room.Tiles.Add(coord);
            dungeon.AddRoom(room);

            // Update map visuals
            _mapPresenter?.OnTileDug(coord);

            // Create room visuals
            var roomPresenter = _session.PresentationFactory.CreateRoomPresenter(roomId, _selectedRoomDef.AssetId);
            roomPresenter.OnRoomPlaced(roomId, _selectedRoomDef.AssetId, new[] { coord });

            GD.Print($"Built new {_selectedRoomDef.Name} at ({coord.X}, {coord.Y})");
        }
    }

    private RoomInstance? FindAdjacentRoom(TileCoordinate coord, RoomType type, EntityId ownerId)
    {
        foreach (var neighbor in coord.GetNeighbors())
        {
            var tile = _session!.Map.GetTile(neighbor);
            if (tile == null) continue;
            if (tile.RoomType != type || tile.RoomInstanceId == null) continue;

            // Find the room instance
            foreach (var room in _session.Players[0].Dungeon.OwnedRooms)
            {
                if (room.Id == tile.RoomInstanceId && room.Type == type && room.OwnerId == ownerId)
                    return room;
            }
        }
        return null;
    }

    private EntityId? FindCreatureAtTile(TileCoordinate coord)
    {
        if (_session == null) return null;
        foreach (var player in _session.Players)
        {
            foreach (var creatureId in player.Dungeon.OwnedCreatureIds)
            {
                var entity = _session.Entities.TryGet(creatureId);
                if (entity == null) continue;
                var movement = entity.TryGetComponent<MovementComponent>();
                if (movement?.CurrentPosition == coord) return creatureId;
            }
        }
        // Also check heroes
        foreach (var entity in _session.Entities.GetAll())
        {
            var identity = entity.TryGetComponent<CreatureIdentityComponent>();
            if (identity == null || identity.Faction != CreatureFaction.Hero) continue;
            var movement = entity.TryGetComponent<MovementComponent>();
            if (movement?.CurrentPosition == coord) return entity.Id;
        }
        return null;
    }

    private TileCoordinate? RaycastToTile(Vector2 screenPos)
    {
        var camera = GetViewport().GetCamera3D();
        if (camera == null) return null;

        var from = camera.ProjectRayOrigin(screenPos);
        var direction = camera.ProjectRayNormal(screenPos);
        var to = from + direction * 200f;

        var spaceState = GetWorld3D().DirectSpaceState;
        var query = PhysicsRayQueryParameters3D.Create(from, to);
        var result = spaceState.IntersectRay(query);

        if (result.Count == 0) return null;

        var hitPos = (Vector3)result["position"];
        return CoordinateHelper.WorldToTile(hitPos);
    }
}

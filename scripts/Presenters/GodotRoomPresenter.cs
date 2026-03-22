using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Core.Presentation;
using DungeonKeeper.Scripts.Rendering;
using Godot;

namespace DungeonKeeper.Scripts.Presenters;

public class GodotRoomPresenter : IRoomPresenter
{
    private readonly Node3D _roomsRoot;
    private readonly Dictionary<EntityId, List<MeshInstance3D>> _roomMeshes = new();

    private static readonly Dictionary<string, Color> RoomColors = new()
    {
        { "DungeonHeart", new Color(0.8f, 0.0f, 0.0f) },
        { "Lair", new Color(0.545f, 0.412f, 0.078f) },
        { "Hatchery", new Color(1.0f, 0.882f, 0.208f) },
        { "Library", new Color(0.275f, 0.510f, 0.706f) },
        { "TrainingRoom", new Color(1.0f, 0.549f, 0.0f) },
        { "CombatPit", new Color(0.7f, 0.2f, 0.2f) },
        { "Treasury", new Color(1.0f, 0.843f, 0.0f) },
        { "Workshop", new Color(0.663f, 0.663f, 0.663f) },
        { "GuardRoom", new Color(0.5f, 0.3f, 0.1f) },
        { "Casino", new Color(0.0f, 0.8f, 0.4f) },
        { "Prison", new Color(0.3f, 0.3f, 0.3f) },
        { "TortureChamber", new Color(0.4f, 0.0f, 0.0f) },
        { "Graveyard", new Color(0.2f, 0.3f, 0.2f) },
        { "Temple", new Color(0.6f, 0.5f, 0.8f) },
        { "WoodenBridge", new Color(0.6f, 0.4f, 0.2f) },
        { "StoneBridge", new Color(0.5f, 0.5f, 0.5f) },
        { "Portal", new Color(1.0f, 0.0f, 1.0f) },
    };

    public GodotRoomPresenter(Node3D roomsRoot)
    {
        _roomsRoot = roomsRoot;
    }

    public void OnRoomPlaced(EntityId roomId, string roomType, IReadOnlyList<TileCoordinate> tiles)
    {
        var color = RoomColors.GetValueOrDefault(roomType, new Color(0.5f, 0.5f, 0.5f));
        var meshes = new List<MeshInstance3D>();

        foreach (var tile in tiles)
        {
            var mesh = PrimitiveMeshFactory.CreateFloorPlane(color);
            mesh.Position = CoordinateHelper.TileToWorld(tile, 0.03f);
            _roomsRoot.AddChild(mesh);
            meshes.Add(mesh);
        }

        _roomMeshes[roomId] = meshes;
    }

    public void OnRoomExpanded(EntityId roomId, IReadOnlyList<TileCoordinate> newTiles)
    {
        if (!_roomMeshes.TryGetValue(roomId, out var meshes)) return;

        // Determine color from existing meshes
        var existingMaterial = PrimitiveMeshFactory.GetMaterial(meshes[0]);
        var color = existingMaterial.AlbedoColor;

        foreach (var tile in newTiles)
        {
            var mesh = PrimitiveMeshFactory.CreateFloorPlane(color);
            mesh.Position = CoordinateHelper.TileToWorld(tile, 0.03f);
            _roomsRoot.AddChild(mesh);
            meshes.Add(mesh);
        }
    }

    public void OnRoomSold(EntityId roomId)
    {
        if (!_roomMeshes.TryGetValue(roomId, out var meshes)) return;

        foreach (var mesh in meshes)
        {
            mesh.QueueFree();
        }

        _roomMeshes.Remove(roomId);
    }

    public void OnWorkerEntered(EntityId roomId, EntityId creatureId)
    {
        // Could add a count indicator later
    }

    public void OnWorkerExited(EntityId roomId, EntityId creatureId)
    {
        // Could add a count indicator later
    }
}

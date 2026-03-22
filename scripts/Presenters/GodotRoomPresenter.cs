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

    private static readonly Dictionary<string, (Color Color, float Height)> RoomStyles = new()
    {
        { "DungeonHeart", (new Color(0.8f, 0.0f, 0.0f), 0.6f) },
        { "Lair", (new Color(0.545f, 0.412f, 0.078f), 0.2f) },
        { "Hatchery", (new Color(1.0f, 0.882f, 0.208f), 0.25f) },
        { "Library", (new Color(0.275f, 0.510f, 0.706f), 0.35f) },
        { "TrainingRoom", (new Color(1.0f, 0.549f, 0.0f), 0.3f) },
        { "CombatPit", (new Color(0.7f, 0.2f, 0.2f), 0.25f) },
        { "Treasury", (new Color(1.0f, 0.843f, 0.0f), 0.3f) },
        { "Workshop", (new Color(0.663f, 0.663f, 0.663f), 0.35f) },
        { "GuardRoom", (new Color(0.5f, 0.3f, 0.1f), 0.3f) },
        { "Casino", (new Color(0.0f, 0.8f, 0.4f), 0.25f) },
        { "Prison", (new Color(0.3f, 0.3f, 0.3f), 0.35f) },
        { "TortureChamber", (new Color(0.4f, 0.0f, 0.0f), 0.3f) },
        { "Graveyard", (new Color(0.2f, 0.3f, 0.2f), 0.15f) },
        { "Temple", (new Color(0.6f, 0.5f, 0.8f), 0.4f) },
        { "WoodenBridge", (new Color(0.6f, 0.4f, 0.2f), 0.1f) },
        { "StoneBridge", (new Color(0.5f, 0.5f, 0.5f), 0.12f) },
        { "Portal", (new Color(1.0f, 0.0f, 1.0f), 0.4f) },
    };

    public GodotRoomPresenter(Node3D roomsRoot)
    {
        _roomsRoot = roomsRoot;
    }

    public void OnRoomPlaced(EntityId roomId, string roomType, IReadOnlyList<TileCoordinate> tiles)
    {
        var (color, height) = RoomStyles.GetValueOrDefault(roomType, (new Color(0.5f, 0.5f, 0.5f), 0.25f));
        var meshes = new List<MeshInstance3D>();

        foreach (var tile in tiles)
        {
            var mesh = CreateRoomMesh(roomType, color, height);
            // Position structure so it sits on top of the tile surface (tile base is at y=0.025)
            mesh.Position = CoordinateHelper.TileToWorld(tile, 0.025f + height / 2f);
            _roomsRoot.AddChild(mesh);
            meshes.Add(mesh);
        }

        _roomMeshes[roomId] = meshes;
    }

    private static MeshInstance3D CreateRoomMesh(string roomType, Color color, float height)
    {
        return roomType switch
        {
            "DungeonHeart" => PrimitiveMeshFactory.CreateDungeonHeart(color),
            "Portal" => PrimitiveMeshFactory.CreatePortalStructure(color),
            _ => PrimitiveMeshFactory.CreateRoomBlock(color, height),
        };
    }

    public void OnRoomExpanded(EntityId roomId, IReadOnlyList<TileCoordinate> newTiles)
    {
        if (!_roomMeshes.TryGetValue(roomId, out var meshes) || meshes.Count == 0) return;

        // Determine color and height from existing mesh
        var existingMaterial = PrimitiveMeshFactory.GetMaterial(meshes[0]);
        var color = existingMaterial.AlbedoColor;

        // Infer height from existing mesh position (it was placed at 0.025 + height/2)
        float existingY = meshes[0].Position.Y;
        float height = (existingY - 0.025f) * 2f;
        if (height < 0.1f) height = 0.25f; // fallback

        // Determine room type from the mesh type
        string roomType = meshes[0].Mesh is CylinderMesh ? "Portal" : "default";

        foreach (var tile in newTiles)
        {
            var mesh = roomType == "Portal"
                ? PrimitiveMeshFactory.CreatePortalStructure(color)
                : PrimitiveMeshFactory.CreateRoomBlock(color, height);
            mesh.Position = CoordinateHelper.TileToWorld(tile, 0.025f + height / 2f);
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

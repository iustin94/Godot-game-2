using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Core.Presentation;
using DungeonKeeper.Dungeon.Map;
using DungeonKeeper.Scripts.Rendering;
using Godot;

namespace DungeonKeeper.Scripts.Presenters;

public class GodotMapPresenter : IMapPresenter
{
    private readonly Node3D _mapRoot;
    private readonly Dictionary<TileCoordinate, MeshInstance3D> _tileMeshes = new();

    private static readonly Dictionary<TileType, (Color Color, float Height)> TileAppearance = new()
    {
        { TileType.Earth, (new Color(0.545f, 0.271f, 0.075f), 0.5f) },
        { TileType.Gold, (new Color(1.0f, 0.843f, 0.0f), 0.5f) },
        { TileType.Gem, (new Color(0.580f, 0.0f, 0.827f), 0.5f) },
        { TileType.ClaimedPath, (new Color(0.5f, 0.5f, 0.5f), 0.05f) },
        { TileType.Water, (new Color(0.255f, 0.412f, 0.882f), -0.1f) },
        { TileType.Lava, (new Color(1.0f, 0.271f, 0.0f), -0.05f) },
        { TileType.Reinforced, (new Color(0.25f, 0.25f, 0.25f), 0.5f) },
        { TileType.Impenetrable, (new Color(0.1f, 0.1f, 0.1f), 0.6f) },
        { TileType.Room, (new Color(0.6f, 0.6f, 0.6f), 0.05f) },
    };

    // Player colors for claimed territory
    private static readonly Color PlayerColor = new(0.3f, 0.5f, 0.8f);

    // Marked-for-digging overlay color (bright tint over existing)
    private static readonly Color MarkedTintColor = new(1.0f, 1.0f, 0.4f);
    private readonly Dictionary<TileCoordinate, Color> _originalColors = new();

    public GodotMapPresenter(Node3D mapRoot)
    {
        _mapRoot = mapRoot;
    }

    public void RenderFullMap(DungeonMap map)
    {
        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                var coord = new TileCoordinate(x, y);
                var tile = map.GetTile(coord);
                if (tile == null) continue;

                var (color, height) = GetTileAppearance(tile.Type);
                var meshInstance = PrimitiveMeshFactory.CreateTileBox(color, Mathf.Max(height, 0.05f));
                meshInstance.Position = CoordinateHelper.TileToWorld(coord, height / 2f);

                _mapRoot.AddChild(meshInstance);
                _tileMeshes[coord] = meshInstance;
            }
        }
    }

    public void OnTileDug(TileCoordinate coord)
    {
        UpdateTile(coord, TileType.ClaimedPath);
    }

    public void OnTileClaimed(TileCoordinate coord, EntityId ownerId)
    {
        if (_tileMeshes.TryGetValue(coord, out var mesh))
        {
            var material = PrimitiveMeshFactory.GetMaterial(mesh);
            material.AlbedoColor = PlayerColor;
        }
    }

    public void OnTileReinforced(TileCoordinate coord)
    {
        UpdateTile(coord, TileType.Reinforced);
    }

    public void OnFogOfWarRevealed(TileCoordinate coord)
    {
        if (_tileMeshes.TryGetValue(coord, out var mesh))
        {
            mesh.Visible = true;
        }
    }

    public void OnTileMarkedForDigging(TileCoordinate coord)
    {
        if (!_tileMeshes.TryGetValue(coord, out var mesh)) return;

        var material = PrimitiveMeshFactory.GetMaterial(mesh);
        _originalColors[coord] = material.AlbedoColor;
        material.AlbedoColor = material.AlbedoColor.Lerp(MarkedTintColor, 0.5f);
        material.Emission = MarkedTintColor;
        material.EmissionEnergyMultiplier = 0.3f;
    }

    public void OnTileUnmarked(TileCoordinate coord)
    {
        if (!_tileMeshes.TryGetValue(coord, out var mesh)) return;

        var material = PrimitiveMeshFactory.GetMaterial(mesh);
        if (_originalColors.TryGetValue(coord, out var original))
        {
            material.AlbedoColor = original;
            _originalColors.Remove(coord);
        }
        material.Emission = new Color(0, 0, 0);
        material.EmissionEnergyMultiplier = 0f;
    }

    private void UpdateTile(TileCoordinate coord, TileType newType)
    {
        if (!_tileMeshes.TryGetValue(coord, out var oldMesh)) return;

        var (color, height) = GetTileAppearance(newType);
        var material = PrimitiveMeshFactory.GetMaterial(oldMesh);
        material.AlbedoColor = color;

        var boxMesh = (BoxMesh)oldMesh.Mesh;
        boxMesh.Size = new Vector3(1.0f, Mathf.Max(height, 0.05f), 1.0f);
        oldMesh.Position = CoordinateHelper.TileToWorld(coord, height / 2f);
    }

    private static (Color Color, float Height) GetTileAppearance(TileType type)
    {
        return TileAppearance.TryGetValue(type, out var appearance)
            ? appearance
            : (new Color(0.5f, 0.5f, 0.5f), 0.05f);
    }
}

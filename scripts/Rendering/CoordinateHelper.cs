using DungeonKeeper.Core.Common;
using Godot;

namespace DungeonKeeper.Scripts.Rendering;

public static class CoordinateHelper
{
    /// <summary>
    /// Converts a tile coordinate to a Godot world position centered on the tile.
    /// TileCoordinate(x,y) maps to Vector3(x + 0.5, 0, y + 0.5).
    /// </summary>
    public static Vector3 TileToWorld(TileCoordinate coord, float height = 0f)
    {
        return new Vector3(coord.X + 0.5f, height, coord.Y + 0.5f);
    }

    /// <summary>
    /// Converts a Godot world position to the nearest tile coordinate.
    /// </summary>
    public static TileCoordinate WorldToTile(Vector3 worldPos)
    {
        return new TileCoordinate((int)worldPos.X, (int)worldPos.Z);
    }
}

using DungeonKeeper.Core.Common;
using DungeonKeeper.Dungeon.Map;

namespace DungeonKeeper.Campaign.MapBlueprint;

public class MapBlueprint
{
    public int Width { get; init; } = 85;
    public int Height { get; init; } = 85;

    private readonly List<TileBlueprintEntry> _tiles = new();
    public IReadOnlyList<TileBlueprintEntry> Tiles => _tiles;

    public void AddTile(TileBlueprintEntry entry) => _tiles.Add(entry);

    public void AddImpenetrableBorder(int thickness = 2)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (x < thickness || x >= Width - thickness || y < thickness || y >= Height - thickness)
                {
                    _tiles.Add(new TileBlueprintEntry
                    {
                        Coordinate = new TileCoordinate(x, y),
                        Type = TileType.Impenetrable
                    });
                }
            }
        }
    }

    public void AddGoldCluster(TileCoordinate center, int count, Random rng, int goldPerTile = 1000)
    {
        for (int i = 0; i < count; i++)
        {
            int dx = rng.Next(-2, 3);
            int dy = rng.Next(-2, 3);
            var coord = new TileCoordinate(center.X + dx, center.Y + dy);
            if (coord.X >= 0 && coord.X < Width && coord.Y >= 0 && coord.Y < Height)
            {
                _tiles.Add(new TileBlueprintEntry
                {
                    Coordinate = coord,
                    Type = TileType.Gold,
                    GoldRemaining = goldPerTile
                });
            }
        }
    }

    public void AddGemCluster(TileCoordinate center, int count, Random rng)
    {
        for (int i = 0; i < count; i++)
        {
            int dx = rng.Next(-1, 2);
            int dy = rng.Next(-1, 2);
            var coord = new TileCoordinate(center.X + dx, center.Y + dy);
            if (coord.X >= 0 && coord.X < Width && coord.Y >= 0 && coord.Y < Height)
            {
                _tiles.Add(new TileBlueprintEntry
                {
                    Coordinate = coord,
                    Type = TileType.Gem
                });
            }
        }
    }

    public void AddPool(TileCoordinate center, TileType type, int radius)
    {
        for (int dx = -radius; dx <= radius; dx++)
        {
            for (int dy = -radius; dy <= radius; dy++)
            {
                if (dx * dx + dy * dy > radius * radius) continue;
                var coord = new TileCoordinate(center.X + dx, center.Y + dy);
                if (coord.X >= 0 && coord.X < Width && coord.Y >= 0 && coord.Y < Height)
                {
                    _tiles.Add(new TileBlueprintEntry
                    {
                        Coordinate = coord,
                        Type = type
                    });
                }
            }
        }
    }
}

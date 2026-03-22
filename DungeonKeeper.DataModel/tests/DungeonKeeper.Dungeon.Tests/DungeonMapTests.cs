using DungeonKeeper.Core.Common;
using DungeonKeeper.Dungeon.Map;

namespace DungeonKeeper.Dungeon.Tests;

public class DungeonMapTests
{
    [Fact]
    public void Constructor_creates_map_with_correct_dimensions()
    {
        var map = new DungeonMap(10, 8);

        Assert.Equal(10, map.Width);
        Assert.Equal(8, map.Height);
    }

    [Fact]
    public void GetTile_returns_correct_tile()
    {
        var map = new DungeonMap(5, 5);

        var tile = map.GetTile(new TileCoordinate(2, 3));

        Assert.NotNull(tile);
        Assert.Equal(new TileCoordinate(2, 3), tile.Coordinate);
    }

    [Fact]
    public void Tiles_initialize_with_Earth_type()
    {
        var map = new DungeonMap(3, 3);

        var tile = map.GetTile(new TileCoordinate(0, 0));

        Assert.NotNull(tile);
        Assert.Equal(TileType.Earth, tile.Type);
    }
}

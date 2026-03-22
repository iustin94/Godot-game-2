using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Dungeon.Map;
using DungeonKeeper.Dungeon.Territory;

namespace DungeonKeeper.Dungeon.Tests;

public class TerritoryManagerTests
{
    [Fact]
    public void ClaimTile_changes_ownership()
    {
        var map = new DungeonMap(10, 10);
        var manager = new TerritoryManager(map);
        var owner = EntityId.New();

        var result = manager.ClaimTile(new TileCoordinate(2, 2), owner);

        Assert.True(result);
        var tile = map.GetTile(new TileCoordinate(2, 2));
        Assert.Equal(owner, tile!.OwnerId);
    }

    [Fact]
    public void Can_query_owned_tile_count()
    {
        var map = new DungeonMap(10, 10);
        var manager = new TerritoryManager(map);
        var owner = EntityId.New();

        manager.ClaimTile(new TileCoordinate(0, 0), owner);
        manager.ClaimTile(new TileCoordinate(1, 0), owner);
        manager.ClaimTile(new TileCoordinate(2, 0), owner);

        Assert.Equal(3, manager.GetOwnedTileCount(owner));
    }
}

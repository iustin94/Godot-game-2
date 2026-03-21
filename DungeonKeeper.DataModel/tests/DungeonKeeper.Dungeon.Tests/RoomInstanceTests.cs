using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Dungeon.Rooms;

namespace DungeonKeeper.Dungeon.Tests;

public class RoomInstanceTests
{
    [Fact]
    public void IsOperational_when_tile_count_meets_minimum()
    {
        var room = new RoomInstance
        {
            Id = EntityId.New(),
            Type = RoomType.Lair,
            MinimumSize = 3
        };
        room.Tiles.Add(new TileCoordinate(0, 0));
        room.Tiles.Add(new TileCoordinate(1, 0));
        room.Tiles.Add(new TileCoordinate(2, 0));

        Assert.True(room.IsOperational);
    }

    [Fact]
    public void IsOperational_false_when_below_minimum()
    {
        var room = new RoomInstance
        {
            Id = EntityId.New(),
            Type = RoomType.Lair,
            MinimumSize = 5
        };
        room.Tiles.Add(new TileCoordinate(0, 0));
        room.Tiles.Add(new TileCoordinate(1, 0));

        Assert.False(room.IsOperational);
    }
}

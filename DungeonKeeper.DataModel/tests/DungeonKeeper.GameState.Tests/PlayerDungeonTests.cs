using DungeonKeeper.Core.Entities;
using DungeonKeeper.Dungeon.Rooms;
using DungeonKeeper.GameState;

namespace DungeonKeeper.GameState.Tests;

public class PlayerDungeonTests
{
    [Fact]
    public void HasRoom_returns_false_when_no_rooms()
    {
        var dungeon = new PlayerDungeon { OwnerId = EntityId.New() };

        Assert.False(dungeon.HasRoom(RoomType.Lair));
    }

    [Fact]
    public void Can_add_and_track_creatures()
    {
        var dungeon = new PlayerDungeon { OwnerId = EntityId.New() };
        var creatureId = EntityId.New();

        dungeon.AddCreature(creatureId);

        Assert.Single(dungeon.OwnedCreatureIds);
        Assert.Equal(creatureId, dungeon.OwnedCreatureIds[0]);
    }
}

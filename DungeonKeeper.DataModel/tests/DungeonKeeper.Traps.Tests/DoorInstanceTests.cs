using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Traps;

namespace DungeonKeeper.Traps.Tests;

public class DoorInstanceTests
{
    [Fact]
    public void Door_can_be_created_with_expected_values()
    {
        var id = EntityId.New();
        var ownerId = EntityId.New();
        var pos = new TileCoordinate(5, 10);

        var door = new DoorInstance
        {
            Id = id,
            DoorDefinitionId = "wooden-door",
            Position = pos,
            OwnerId = ownerId,
            CurrentHealth = 100f,
            IsOpen = false,
            IsLocked = true
        };

        Assert.Equal(id, door.Id);
        Assert.Equal("wooden-door", door.DoorDefinitionId);
        Assert.Equal(pos, door.Position);
        Assert.Equal(ownerId, door.OwnerId);
        Assert.Equal(100f, door.CurrentHealth);
        Assert.False(door.IsOpen);
        Assert.True(door.IsLocked);
    }

    [Fact]
    public void Door_can_be_opened_and_closed()
    {
        var door = new DoorInstance
        {
            Id = EntityId.New(),
            DoorDefinitionId = "wooden-door",
            Position = new TileCoordinate(0, 0),
            OwnerId = EntityId.New()
        };

        Assert.False(door.IsOpen);

        door.IsOpen = true;
        Assert.True(door.IsOpen);

        door.IsOpen = false;
        Assert.False(door.IsOpen);
    }
}

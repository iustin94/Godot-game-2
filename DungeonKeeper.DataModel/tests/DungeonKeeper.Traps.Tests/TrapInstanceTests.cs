using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Traps;

namespace DungeonKeeper.Traps.Tests;

public class TrapInstanceTests
{
    [Fact]
    public void Trap_initializes_in_Armed_state()
    {
        var trap = new TrapInstance
        {
            Id = EntityId.New(),
            TrapDefinitionId = "spike-trap",
            Position = new TileCoordinate(3, 7),
            OwnerId = EntityId.New(),
            Health = 100f
        };

        Assert.Equal(TrapState.Armed, trap.State);
    }
}

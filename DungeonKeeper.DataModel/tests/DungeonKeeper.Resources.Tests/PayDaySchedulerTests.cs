using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Resources;
using DungeonKeeper.Resources.PayDay;

namespace DungeonKeeper.Resources.Tests;

public class PayDaySchedulerTests
{
    [Fact]
    public void ProcessPayDay_pays_creatures_and_returns_result()
    {
        var scheduler = new PayDayScheduler();
        var gold = new ResourcePool { Type = ResourceType.Gold, Current = 100, Capacity = 1000 };
        var creatures = new List<(EntityId, int)>
        {
            (EntityId.New(), 30),
            (EntityId.New(), 40),
        };

        var result = scheduler.ProcessPayDay(creatures, gold);

        Assert.Equal(70, result.TotalPaid);
        Assert.Equal(2, result.PaidCreatures.Count);
        Assert.Empty(result.UnpaidCreatures);
        Assert.Equal(30, gold.Current);
    }

    [Fact]
    public void Unpaid_creatures_are_tracked_when_gold_runs_out()
    {
        var scheduler = new PayDayScheduler();
        var gold = new ResourcePool { Type = ResourceType.Gold, Current = 30, Capacity = 1000 };
        var c1 = EntityId.New();
        var c2 = EntityId.New();
        var creatures = new List<(EntityId, int)>
        {
            (c1, 30),
            (c2, 50),
        };

        var result = scheduler.ProcessPayDay(creatures, gold);

        Assert.Single(result.PaidCreatures);
        Assert.Equal(c1, result.PaidCreatures[0]);
        Assert.Single(result.UnpaidCreatures);
        Assert.Equal(c2, result.UnpaidCreatures[0]);
        Assert.Equal(30, result.TotalPaid);
    }
}

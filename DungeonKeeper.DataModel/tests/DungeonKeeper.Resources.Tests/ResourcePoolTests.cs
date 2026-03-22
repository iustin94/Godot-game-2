using DungeonKeeper.Core.Common;
using DungeonKeeper.Resources;

namespace DungeonKeeper.Resources.Tests;

public class ResourcePoolTests
{
    private ResourcePool CreatePool(int current = 100, int capacity = 200) =>
        new() { Type = ResourceType.Gold, Current = current, Capacity = capacity };

    [Fact]
    public void CanAfford_returns_true_when_enough()
    {
        var pool = CreatePool(current: 50);

        Assert.True(pool.CanAfford(50));
    }

    [Fact]
    public void CanAfford_returns_false_when_not_enough()
    {
        var pool = CreatePool(current: 10);

        Assert.False(pool.CanAfford(20));
    }

    [Fact]
    public void TrySpend_reduces_current()
    {
        var pool = CreatePool(current: 100);

        var result = pool.TrySpend(30);

        Assert.True(result);
        Assert.Equal(70, pool.Current);
    }

    [Fact]
    public void TrySpend_returns_false_and_does_not_change_if_insufficient()
    {
        var pool = CreatePool(current: 10);

        var result = pool.TrySpend(20);

        Assert.False(result);
        Assert.Equal(10, pool.Current);
    }

    [Fact]
    public void Add_clamps_to_capacity()
    {
        var pool = CreatePool(current: 180, capacity: 200);

        pool.Add(50);

        Assert.Equal(200, pool.Current);
    }
}

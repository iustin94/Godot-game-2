using DungeonKeeper.Creatures.Components;

namespace DungeonKeeper.Creatures.Tests;

public class StatsComponentTests
{
    [Fact]
    public void IsAlive_ReturnsTrue_WhenHealthGreaterThanZero()
    {
        var stats = new StatsComponent { CurrentHealth = 50, MaxHealth = 100 };

        Assert.True(stats.IsAlive);
    }

    [Fact]
    public void IsAlive_ReturnsFalse_WhenHealthIsZero()
    {
        var stats = new StatsComponent { CurrentHealth = 0, MaxHealth = 100 };

        Assert.False(stats.IsAlive);
    }

    [Fact]
    public void IsAlive_ReturnsFalse_WhenHealthIsNegative()
    {
        var stats = new StatsComponent { CurrentHealth = -10, MaxHealth = 100 };

        Assert.False(stats.IsAlive);
    }
}

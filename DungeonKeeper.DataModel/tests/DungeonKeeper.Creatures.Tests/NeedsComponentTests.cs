using DungeonKeeper.Creatures.Components;

namespace DungeonKeeper.Creatures.Tests;

public class NeedsComponentTests
{
    [Fact]
    public void DefaultValues_AreReasonable()
    {
        var needs = new NeedsComponent();

        Assert.Equal(0f, needs.Hunger);
        Assert.Equal(0f, needs.Tiredness);
        Assert.Equal(1f, needs.PaySatisfaction);
        Assert.Equal(1f, needs.Happiness);
        Assert.False(needs.HasLair);
        Assert.Equal(0, needs.TicksSinceLastPaid);
        Assert.Equal(0, needs.TicksSinceLastMeal);
    }
}

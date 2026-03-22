using DungeonKeeper.Creatures.Components;

namespace DungeonKeeper.Creatures.Tests;

public class MoraleComponentTests
{
    [Fact]
    public void DefaultMoraleState_IsContent()
    {
        var morale = new MoraleComponent();

        Assert.Equal(MoraleState.Content, morale.State);
        Assert.Equal(1f, morale.Morale);
        Assert.Equal(0, morale.AngerTicks);
    }
}

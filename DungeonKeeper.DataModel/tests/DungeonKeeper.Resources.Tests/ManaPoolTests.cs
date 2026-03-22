using DungeonKeeper.Resources;

namespace DungeonKeeper.Resources.Tests;

public class ManaPoolTests
{
    [Fact]
    public void NetRate_is_GenerationRate_minus_DrainRate()
    {
        var pool = new ManaPool
        {
            GenerationRate = 10f,
            DrainRate = 3f
        };

        Assert.Equal(7f, pool.NetRate);
    }
}

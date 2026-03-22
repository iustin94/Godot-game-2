using DungeonKeeper.Core.Entities;
using DungeonKeeper.Keeper.Possession;

namespace DungeonKeeper.Keeper.Tests;

public class PossessionSessionTests
{
    [Fact]
    public void IsActive_defaults_correctly()
    {
        var session = new PossessionSession
        {
            PossessedCreatureId = EntityId.New(),
            KeeperId = EntityId.New()
        };

        Assert.False(session.IsActive);
    }

    [Fact]
    public void ManaCost_values_are_set()
    {
        var session = new PossessionSession
        {
            PossessedCreatureId = EntityId.New(),
            KeeperId = EntityId.New(),
            InitialManaCost = 1000,
            OngoingManaDrainPerSecond = 50f
        };

        Assert.Equal(1000, session.InitialManaCost);
        Assert.Equal(50f, session.OngoingManaDrainPerSecond);
    }
}

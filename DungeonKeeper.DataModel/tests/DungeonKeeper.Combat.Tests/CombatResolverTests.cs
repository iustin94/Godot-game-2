using DungeonKeeper.Combat;
using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Combat.Tests;

public class CombatResolverTests
{
    [Fact]
    public void ResolveTick_ReturnsCombatRound()
    {
        var calc = new DungeonKeeperDamageCalculator(new Random(42));
        var resolver = new CombatResolver(calc);

        var participants = new List<CombatParticipant>
        {
            new(EntityId.New(), EntityId.New(), new TileCoordinate(0, 0)),
            new(EntityId.New(), EntityId.New(), new TileCoordinate(1, 0))
        };
        var time = new GameTime(1, 0.1f, 0.1f);

        var round = resolver.ResolveTick(participants, time);

        Assert.NotNull(round);
        Assert.IsType<CombatRound>(round);
    }

    [Fact]
    public void ResolveTick_EmptyParticipants_ReturnsEmptyRound()
    {
        var calc = new DungeonKeeperDamageCalculator(new Random(42));
        var resolver = new CombatResolver(calc);

        var participants = new List<CombatParticipant>();
        var time = new GameTime(1, 0.1f, 0.1f);

        var round = resolver.ResolveTick(participants, time);

        Assert.NotNull(round);
        Assert.Empty(round.Attacks);
        Assert.Empty(round.Casualties);
        Assert.Empty(round.FledCreatures);
    }
}

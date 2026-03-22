using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Spells;
using DungeonKeeper.Spells.Effects;

namespace DungeonKeeper.Spells.Tests;

public class SpellCastTests
{
    [Fact]
    public void CreateImpEffect_ReturnsSuccess()
    {
        var effect = new CreateImpEffect();
        var context = new SpellCastContext(
            CasterId: EntityId.New(),
            TargetTile: new TileCoordinate(5, 5),
            TargetCreature: null);

        var result = effect.Apply(context);

        Assert.True(result.Success);
        Assert.Null(result.FailureReason);
    }

    [Fact]
    public void HealEffect_ReturnsSuccess()
    {
        var effect = new HealEffect(healAmount: 100);
        var context = new SpellCastContext(
            CasterId: EntityId.New(),
            TargetTile: null,
            TargetCreature: EntityId.New());

        var result = effect.Apply(context);

        Assert.True(result.Success);
        Assert.Null(result.FailureReason);
    }
}

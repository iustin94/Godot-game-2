using DungeonKeeper.Spells;
using DungeonKeeper.Spells.Effects;

namespace DungeonKeeper.Spells.Tests;

public class SpellDefinitionRegistryTests
{
    [Fact]
    public void Register_And_Get_WorkCorrectly()
    {
        var registry = new SpellDefinitionRegistry();
        var spell = new SpellDefinition
        {
            Id = "create_imp",
            Name = "Create Imp",
            AssetId = "create_imp",
            Type = SpellType.CreateImp,
            ManaCost = 150,
            Cooldown = 1f,
            TargetType = SpellTargetType.None,
            Effect = new CreateImpEffect()
        };

        registry.Register(spell);

        var retrieved = registry.Get("create_imp");
        Assert.Equal("Create Imp", retrieved.Name);
        Assert.Equal(SpellType.CreateImp, retrieved.Type);
        Assert.Equal(150, retrieved.ManaCost);
    }

    [Fact]
    public void Get_ThrowsKeyNotFoundException_ForUnregisteredSpell()
    {
        var registry = new SpellDefinitionRegistry();

        Assert.Throws<KeyNotFoundException>(() => registry.Get("nonexistent"));
    }
}

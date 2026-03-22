using DungeonKeeper.Creatures.Data;
using DungeonKeeper.Creatures.Definitions;

namespace DungeonKeeper.Creatures.Tests;

public class DK2CreatureDataTests
{
    private readonly CreatureDefinitionRegistry _registry;

    public DK2CreatureDataTests()
    {
        _registry = new CreatureDefinitionRegistry();
        DK2CreatureData.RegisterAll(_registry);
    }

    [Fact]
    public void RegisterAll_PopulatesRegistryWithExpectedCreatureCount()
    {
        var all = _registry.GetAll();
        var keepers = _registry.GetByFaction(CreatureFaction.Keeper).ToList();
        var heroes = _registry.GetByFaction(CreatureFaction.Hero).ToList();

        Assert.True(keepers.Count >= 15, $"Expected at least 15 keeper creatures, got {keepers.Count}");
        Assert.True(heroes.Count >= 5, $"Expected at least 5 hero creatures, got {heroes.Count}");
    }

    [Fact]
    public void Imp_IsPresentWithCorrectFaction()
    {
        var imp = _registry.Get(CreatureType.Imp);

        Assert.Equal(CreatureFaction.Keeper, imp.Faction);
        Assert.Equal("Imp", imp.Name);
    }

    [Fact]
    public void Avatar_IsPresentWithCorrectFaction()
    {
        var avatar = _registry.Get(CreatureType.Avatar);

        Assert.Equal(CreatureFaction.Hero, avatar.Faction);
        Assert.Equal("Avatar", avatar.Name);
    }

    [Fact]
    public void Goblin_HasCorrectBaseHP()
    {
        var goblin = _registry.Get(CreatureType.Goblin);

        Assert.Equal(600, goblin.BaseStats.MaxHealth);
    }

    [Fact]
    public void HornedReaper_IsElite()
    {
        var horny = _registry.Get(CreatureType.Horny);

        Assert.True(horny.IsElite);
    }

    [Fact]
    public void Vampire_IsUndeadAndCannotBeAttractedViaPortal()
    {
        var vampire = _registry.Get(CreatureType.Vampire);

        Assert.True(vampire.IsUndead);
        Assert.True(vampire.CannotBeAttractedViaPortal);
    }
}

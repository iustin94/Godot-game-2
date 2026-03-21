using DungeonKeeper.Creatures.Definitions;

namespace DungeonKeeper.Creatures.Tests;

public class CreatureDefinitionRegistryTests
{
    private readonly CreatureDefinitionRegistry _registry = new();

    private CreatureDefinition BuildDefinition(CreatureType type, CreatureFaction faction = CreatureFaction.Keeper)
    {
        return new CreatureDefinitionBuilder()
            .WithType(type)
            .WithFaction(faction)
            .Build();
    }

    [Fact]
    public void Register_And_Get_WorkCorrectly()
    {
        var def = BuildDefinition(CreatureType.Goblin);
        _registry.Register(def);

        var retrieved = _registry.Get(CreatureType.Goblin);
        Assert.Equal(CreatureType.Goblin, retrieved.Type);
    }

    [Fact]
    public void GetAll_ReturnsAllRegistered()
    {
        _registry.Register(BuildDefinition(CreatureType.Goblin));
        _registry.Register(BuildDefinition(CreatureType.Dragon));
        _registry.Register(BuildDefinition(CreatureType.Imp));

        var all = _registry.GetAll();
        Assert.Equal(3, all.Count);
    }

    [Fact]
    public void GetByFaction_FiltersCorrectly()
    {
        _registry.Register(BuildDefinition(CreatureType.Goblin, CreatureFaction.Keeper));
        _registry.Register(BuildDefinition(CreatureType.Dragon, CreatureFaction.Keeper));
        _registry.Register(BuildDefinition(CreatureType.Knight, CreatureFaction.Hero));

        var keepers = _registry.GetByFaction(CreatureFaction.Keeper).ToList();
        var heroes = _registry.GetByFaction(CreatureFaction.Hero).ToList();

        Assert.Equal(2, keepers.Count);
        Assert.Single(heroes);
        Assert.Equal(CreatureType.Knight, heroes[0].Type);
    }
}

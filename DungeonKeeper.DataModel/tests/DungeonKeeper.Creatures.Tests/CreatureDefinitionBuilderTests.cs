using DungeonKeeper.Creatures.Definitions;
using DungeonKeeper.Dungeon.Rooms;

namespace DungeonKeeper.Creatures.Tests;

public class CreatureDefinitionBuilderTests
{
    [Fact]
    public void Build_SetsCorrectTypeAndName()
    {
        var def = new CreatureDefinitionBuilder()
            .WithType(CreatureType.Goblin)
            .WithName("Goblin")
            .Build();

        Assert.Equal(CreatureType.Goblin, def.Type);
        Assert.Equal("Goblin", def.Name);
    }

    [Fact]
    public void Build_SetsBaseStatsCorrectly()
    {
        var stats = new CreatureBaseStats(500, 20, 10, 15, 5, 60, 40f, 2, 3, 30);
        var def = new CreatureDefinitionBuilder()
            .WithType(CreatureType.Warlock)
            .WithBaseStats(stats)
            .Build();

        Assert.Equal(500, def.BaseStats.MaxHealth);
        Assert.Equal(20, def.BaseStats.MeleeAttack);
        Assert.Equal(10, def.BaseStats.MeleeDamage);
        Assert.Equal(15, def.BaseStats.Defense);
        Assert.Equal(5, def.BaseStats.Armor);
        Assert.Equal(60, def.BaseStats.Luck);
        Assert.Equal(40f, def.BaseStats.Speed);
        Assert.Equal(2, def.BaseStats.ResearchSkill);
        Assert.Equal(3, def.BaseStats.ManufactureSkill);
        Assert.Equal(30, def.BaseStats.TrainingCost);
    }

    [Fact]
    public void Build_DefaultsNameToTypeName_WhenNameNotSet()
    {
        var def = new CreatureDefinitionBuilder()
            .WithType(CreatureType.Dragon)
            .Build();

        Assert.Equal("Dragon", def.Name);
    }

    [Fact]
    public void Build_CanSetAllFlags()
    {
        var def = new CreatureDefinitionBuilder()
            .WithType(CreatureType.Vampire)
            .IsUndead()
            .HasFlight()
            .IsImmuneToPoison()
            .CannotBeAttractedViaPortal()
            .Build();

        Assert.True(def.IsUndead);
        Assert.True(def.CanFly);
        Assert.True(def.ImmuneToPoison);
        Assert.True(def.CannotBeAttractedViaPortal);
    }

    [Fact]
    public void Build_CanAddAttractionRequirementsAndJobPreferences()
    {
        var def = new CreatureDefinitionBuilder()
            .WithType(CreatureType.Goblin)
            .WithAttractionRequirement(RoomType.Lair, 1)
            .WithAttractionRequirement(RoomType.TrainingRoom, 9)
            .WithJobPreference(RoomType.TrainingRoom)
            .WithJobPreference(RoomType.GuardRoom)
            .Build();

        Assert.Equal(2, def.AttractionRequirements.Count);
        Assert.Equal(RoomType.Lair, def.AttractionRequirements[0].RoomType);
        Assert.Equal(1, def.AttractionRequirements[0].MinimumSize);
        Assert.Equal(RoomType.TrainingRoom, def.AttractionRequirements[1].RoomType);
        Assert.Equal(9, def.AttractionRequirements[1].MinimumSize);

        Assert.Equal(2, def.JobPreferences.Count);
        Assert.Contains(RoomType.TrainingRoom, def.JobPreferences);
        Assert.Contains(RoomType.GuardRoom, def.JobPreferences);
    }
}

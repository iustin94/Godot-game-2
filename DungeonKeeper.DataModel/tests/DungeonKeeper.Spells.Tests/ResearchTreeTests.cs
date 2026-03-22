using DungeonKeeper.Spells;
using DungeonKeeper.Spells.Effects;

namespace DungeonKeeper.Spells.Tests;

public class ResearchTreeTests
{
    private static SpellDefinition MakeSpell(
        string id,
        int researchPoints = 100,
        bool availableByDefault = false,
        IReadOnlyList<string>? prerequisites = null)
    {
        return new SpellDefinition
        {
            Id = id,
            Name = id,
            AssetId = id,
            Type = SpellType.Heal,
            ManaCost = 50,
            Cooldown = 1f,
            TargetType = SpellTargetType.Creature,
            ResearchPointsRequired = researchPoints,
            AvailableByDefault = availableByDefault,
            Prerequisites = prerequisites ?? Array.Empty<string>(),
            Effect = new HealEffect(50)
        };
    }

    [Fact]
    public void NewTree_HasNothingResearched()
    {
        var tree = new ResearchTree(new[]
        {
            MakeSpell("spell_a", researchPoints: 100),
            MakeSpell("spell_b", researchPoints: 200)
        });

        Assert.False(tree.IsResearched("spell_a"));
        Assert.False(tree.IsResearched("spell_b"));
    }

    [Fact]
    public void AddResearchPoints_Accumulates()
    {
        var tree = new ResearchTree(new[]
        {
            MakeSpell("spell_a", researchPoints: 100)
        });

        tree.AddResearchPoints("spell_a", 30);
        Assert.False(tree.IsResearched("spell_a"));

        tree.AddResearchPoints("spell_a", 30);
        Assert.False(tree.IsResearched("spell_a"));

        tree.AddResearchPoints("spell_a", 40);
        Assert.True(tree.IsResearched("spell_a"));
    }

    [Fact]
    public void IsComplete_ReturnsTrue_WhenPointsReachRequired()
    {
        var tree = new ResearchTree(new[]
        {
            MakeSpell("spell_a", researchPoints: 50)
        });

        tree.AddResearchPoints("spell_a", 50);

        Assert.True(tree.IsResearched("spell_a"));
    }

    [Fact]
    public void SetResearchTarget_And_GetCurrentResearchTarget_Work()
    {
        var tree = new ResearchTree(new[]
        {
            MakeSpell("spell_a", researchPoints: 100),
            MakeSpell("spell_b", researchPoints: 100)
        });

        Assert.Null(tree.GetCurrentResearchTarget());

        tree.SetResearchTarget("spell_a");
        Assert.Equal("spell_a", tree.GetCurrentResearchTarget());
    }

    [Fact]
    public void CanResearch_ChecksPrerequisites()
    {
        var tree = new ResearchTree(new[]
        {
            MakeSpell("spell_a", researchPoints: 50),
            MakeSpell("spell_b", researchPoints: 100, prerequisites: new[] { "spell_a" })
        });

        // spell_b requires spell_a, which is not yet researched
        Assert.False(tree.CanResearch("spell_b"));

        // Research spell_a
        tree.AddResearchPoints("spell_a", 50);
        Assert.True(tree.IsResearched("spell_a"));

        // Now spell_b should be researchable
        Assert.True(tree.CanResearch("spell_b"));
    }

    [Fact]
    public void CanResearch_ReturnsFalse_ForAvailableByDefault()
    {
        var tree = new ResearchTree(new[]
        {
            MakeSpell("spell_a", availableByDefault: true)
        });

        // Available by default means already researched, so cannot be researched again
        Assert.False(tree.CanResearch("spell_a"));
        Assert.True(tree.IsResearched("spell_a"));
    }
}

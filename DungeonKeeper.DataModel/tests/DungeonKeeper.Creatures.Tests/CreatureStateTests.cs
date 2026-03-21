using DungeonKeeper.Creatures.AI;

namespace DungeonKeeper.Creatures.Tests;

public class CreatureStateTests
{
    [Theory]
    [InlineData(nameof(CreatureState.Idle))]
    [InlineData(nameof(CreatureState.GoingToWork))]
    [InlineData(nameof(CreatureState.Working))]
    [InlineData(nameof(CreatureState.GoingToEat))]
    [InlineData(nameof(CreatureState.Eating))]
    [InlineData(nameof(CreatureState.GoingToLair))]
    [InlineData(nameof(CreatureState.Sleeping))]
    [InlineData(nameof(CreatureState.Training))]
    [InlineData(nameof(CreatureState.FightingInCombatPit))]
    [InlineData(nameof(CreatureState.InCombat))]
    [InlineData(nameof(CreatureState.Fleeing))]
    [InlineData(nameof(CreatureState.Stunned))]
    [InlineData(nameof(CreatureState.GoingToCollectPay))]
    [InlineData(nameof(CreatureState.Gambling))]
    [InlineData(nameof(CreatureState.Praying))]
    [InlineData(nameof(CreatureState.Angry))]
    [InlineData(nameof(CreatureState.Leaving))]
    [InlineData(nameof(CreatureState.BeingHeld))]
    [InlineData(nameof(CreatureState.Possessed))]
    [InlineData(nameof(CreatureState.Imprisoned))]
    [InlineData(nameof(CreatureState.BeingTortured))]
    [InlineData(nameof(CreatureState.Dead))]
    public void AllExpectedStatesExist(string stateName)
    {
        Assert.True(Enum.TryParse<CreatureState>(stateName, out _), $"State '{stateName}' should exist in CreatureState enum");
    }

    [Fact]
    public void CreatureState_HasExpectedNumberOfValues()
    {
        var values = Enum.GetValues<CreatureState>();
        Assert.Equal(22, values.Length);
    }
}

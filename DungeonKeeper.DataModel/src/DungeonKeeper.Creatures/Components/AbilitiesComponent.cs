using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Creatures.Components;

public sealed class AbilitiesComponent : IComponent
{
    public List<string> UnlockedAbilityIds { get; init; } = new();
    public Dictionary<string, float> AbilityCooldowns { get; init; } = new();
}

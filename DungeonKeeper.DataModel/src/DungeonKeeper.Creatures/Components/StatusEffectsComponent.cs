using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Creatures.Components;

public record ActiveStatusEffect(string EffectType, float RemainingDuration, float Magnitude);

public sealed class StatusEffectsComponent : IComponent
{
    public List<ActiveStatusEffect> ActiveEffects { get; init; } = new();
}

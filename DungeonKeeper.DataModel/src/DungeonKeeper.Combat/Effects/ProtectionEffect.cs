using DungeonKeeper.Core.Entities;
using DungeonKeeper.Creatures.Components;

namespace DungeonKeeper.Combat.Effects;

/// <summary>
/// Reduces incoming damage by adding bonus armor for the duration.
/// </summary>
public class ProtectionEffect : IStatusEffect
{
    private float _remainingDuration;
    private int _originalArmor;

    public StatusEffectType Type => StatusEffectType.Protection;
    public float Duration { get; }
    public float Magnitude { get; }

    public ProtectionEffect(float duration, float bonusArmor)
    {
        Duration = duration;
        _remainingDuration = duration;
        Magnitude = bonusArmor;
    }

    public void Apply(IEntity target)
    {
        var stats = target.TryGetComponent<StatsComponent>();
        if (stats is null) return;
        _originalArmor = stats.Armor;
        stats.Armor = _originalArmor + (int)Magnitude;
    }

    public void Remove(IEntity target)
    {
        var stats = target.TryGetComponent<StatsComponent>();
        if (stats is null) return;
        stats.Armor = _originalArmor;
    }

    public void Tick(IEntity target, float deltaTime)
    {
        _remainingDuration -= deltaTime;
    }
}

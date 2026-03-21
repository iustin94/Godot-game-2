using DungeonKeeper.Core.Entities;
using DungeonKeeper.Creatures.Components;

namespace DungeonKeeper.Combat.Effects;

/// <summary>
/// Deals damage over time each tick.
/// </summary>
public class PoisonedEffect : IStatusEffect
{
    private float _remainingDuration;

    public StatusEffectType Type => StatusEffectType.Poisoned;
    public float Duration { get; }
    public float Magnitude { get; }

    public PoisonedEffect(float duration, float damagePerSecond)
    {
        Duration = duration;
        _remainingDuration = duration;
        Magnitude = damagePerSecond;
    }

    public void Apply(IEntity target)
    {
        // Poison applies its damage via Tick, no immediate effect.
    }

    public void Remove(IEntity target)
    {
        // No lingering effect after removal.
    }

    public void Tick(IEntity target, float deltaTime)
    {
        _remainingDuration -= deltaTime;
        var stats = target.TryGetComponent<StatsComponent>();
        if (stats is null) return;
        int damage = (int)(Magnitude * deltaTime);
        stats.CurrentHealth = Math.Max(0, stats.CurrentHealth - damage);
    }
}

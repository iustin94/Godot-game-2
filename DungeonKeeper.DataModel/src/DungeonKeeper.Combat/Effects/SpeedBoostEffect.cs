using DungeonKeeper.Core.Entities;
using DungeonKeeper.Creatures.Components;

namespace DungeonKeeper.Combat.Effects;

/// <summary>
/// Multiplies speed by the magnitude for the duration.
/// </summary>
public class SpeedBoostEffect : IStatusEffect
{
    private float _remainingDuration;
    private float _originalSpeed;

    public StatusEffectType Type => StatusEffectType.SpeedBoost;
    public float Duration { get; }
    public float Magnitude { get; }

    public SpeedBoostEffect(float duration, float speedMultiplier)
    {
        Duration = duration;
        _remainingDuration = duration;
        Magnitude = speedMultiplier;
    }

    public void Apply(IEntity target)
    {
        var stats = target.TryGetComponent<StatsComponent>();
        if (stats is null) return;
        _originalSpeed = stats.Speed;
        stats.Speed = _originalSpeed * Magnitude;
    }

    public void Remove(IEntity target)
    {
        var stats = target.TryGetComponent<StatsComponent>();
        if (stats is null) return;
        stats.Speed = _originalSpeed;
    }

    public void Tick(IEntity target, float deltaTime)
    {
        _remainingDuration -= deltaTime;
    }
}

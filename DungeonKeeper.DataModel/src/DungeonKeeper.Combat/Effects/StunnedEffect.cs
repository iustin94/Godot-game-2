using DungeonKeeper.Core.Entities;
using DungeonKeeper.Creatures.Components;

namespace DungeonKeeper.Combat.Effects;

/// <summary>
/// Prevents actions for the duration by setting speed to 0.
/// </summary>
public class StunnedEffect : IStatusEffect
{
    private float _remainingDuration;
    private float _originalSpeed;

    public StatusEffectType Type => StatusEffectType.Stunned;
    public float Duration { get; }
    public float Magnitude { get; }

    public StunnedEffect(float duration)
    {
        Duration = duration;
        _remainingDuration = duration;
        Magnitude = 0f;
    }

    public void Apply(IEntity target)
    {
        var stats = target.TryGetComponent<StatsComponent>();
        if (stats is null) return;
        _originalSpeed = stats.Speed;
        stats.Speed = 0f;
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

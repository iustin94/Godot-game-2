using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Combat.Effects;

public interface IStatusEffect
{
    StatusEffectType Type { get; }
    float Duration { get; }
    float Magnitude { get; }
    void Apply(IEntity target);
    void Remove(IEntity target);
    void Tick(IEntity target, float deltaTime);
}

namespace DungeonKeeper.Combat.Abilities;

public interface IAbility
{
    string Id { get; }
    string Name { get; }
    float Cooldown { get; }
    float Range { get; }
    DamageType? DamageType { get; }
    AbilityTargetType TargetType { get; }
}

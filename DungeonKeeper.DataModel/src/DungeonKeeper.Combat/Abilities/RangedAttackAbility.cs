namespace DungeonKeeper.Combat.Abilities;

public class RangedAttackAbility : IAbility
{
    public string Id => "ranged_attack";
    public string Name => "Ranged Attack";
    public float Cooldown { get; init; } = 1.5f;
    public float Range { get; init; } = 5.0f;
    public DamageType? DamageType => Combat.DamageType.Physical;
    public AbilityTargetType TargetType => AbilityTargetType.SingleTarget;
}

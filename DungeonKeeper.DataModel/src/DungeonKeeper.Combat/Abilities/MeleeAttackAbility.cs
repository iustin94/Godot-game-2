namespace DungeonKeeper.Combat.Abilities;

public class MeleeAttackAbility : IAbility
{
    public string Id => "melee_attack";
    public string Name => "Melee Attack";
    public float Cooldown { get; init; } = 1.0f;
    public float Range => 1.0f;
    public DamageType? DamageType => Combat.DamageType.Physical;
    public AbilityTargetType TargetType => AbilityTargetType.SingleTarget;
}

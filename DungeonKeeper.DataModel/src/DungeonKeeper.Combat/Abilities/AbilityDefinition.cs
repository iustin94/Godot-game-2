namespace DungeonKeeper.Combat.Abilities;

public class AbilityDefinition : IAbility
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public float Cooldown { get; init; }
    public float Range { get; init; }
    public DamageType? DamageType { get; init; }
    public AbilityTargetType TargetType { get; init; }
    public int ManaCost { get; init; }
    public int BaseDamage { get; init; }
    public float AreaRadius { get; init; }
}

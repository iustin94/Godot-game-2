namespace DungeonKeeper.Combat.Abilities;

public class SpellAbility : IAbility
{
    public string Id { get; init; } = "spell";
    public string Name { get; init; } = "Spell";
    public float Cooldown { get; init; } = 3.0f;
    public float Range { get; init; } = 8.0f;
    public DamageType? DamageType { get; init; } = Combat.DamageType.Fire;
    public AbilityTargetType TargetType { get; init; } = AbilityTargetType.AreaOfEffect;
    public int ManaCost { get; init; } = 10;
    public float AreaRadius { get; init; } = 2.0f;
}

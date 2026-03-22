namespace DungeonKeeper.Spells.Effects;

/// <summary>
/// Damages all enemies within the specified area.
/// </summary>
public class DamageAreaEffect : ISpellEffect
{
    public int Damage { get; }
    public float Radius { get; }

    public DamageAreaEffect(int damage, float radius)
    {
        Damage = damage;
        Radius = radius;
    }

    public SpellCastResult Apply(SpellCastContext context)
    {
        // TODO: Damage enemies in area when GameState is available
        return new SpellCastResult(true);
    }
}

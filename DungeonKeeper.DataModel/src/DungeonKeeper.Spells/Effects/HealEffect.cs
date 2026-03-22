namespace DungeonKeeper.Spells.Effects;

/// <summary>
/// Heals the target creature by a specified amount.
/// </summary>
public class HealEffect : ISpellEffect
{
    public int HealAmount { get; }

    public HealEffect(int healAmount)
    {
        HealAmount = healAmount;
    }

    public SpellCastResult Apply(SpellCastContext context)
    {
        // TODO: Heal target creature when GameState is available
        return new SpellCastResult(true);
    }
}

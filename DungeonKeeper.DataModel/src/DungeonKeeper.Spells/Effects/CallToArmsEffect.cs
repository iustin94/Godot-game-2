namespace DungeonKeeper.Spells.Effects;

/// <summary>
/// Rallies owned creatures to the target location.
/// </summary>
public class CallToArmsEffect : ISpellEffect
{
    public SpellCastResult Apply(SpellCastContext context)
    {
        // TODO: Rally creatures to location when GameState is available
        return new SpellCastResult(true);
    }
}

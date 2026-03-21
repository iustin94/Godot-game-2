namespace DungeonKeeper.Spells.Effects;

/// <summary>
/// Converts an enemy creature to the caster's side.
/// </summary>
public class TurncoatEffect : ISpellEffect
{
    public SpellCastResult Apply(SpellCastContext context)
    {
        // TODO: Convert enemy creature when GameState is available
        return new SpellCastResult(true);
    }
}

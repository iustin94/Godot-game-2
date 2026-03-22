namespace DungeonKeeper.Spells.Effects;

/// <summary>
/// Collapses tiles in the target area.
/// </summary>
public class CaveInEffect : ISpellEffect
{
    public SpellCastResult Apply(SpellCastContext context)
    {
        // TODO: Collapse tiles when GameState is available
        return new SpellCastResult(true);
    }
}

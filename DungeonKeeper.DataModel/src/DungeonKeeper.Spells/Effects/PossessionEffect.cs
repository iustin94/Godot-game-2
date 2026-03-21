namespace DungeonKeeper.Spells.Effects;

/// <summary>
/// Possesses the target creature, giving the player first-person control.
/// </summary>
public class PossessionEffect : ISpellEffect
{
    public SpellCastResult Apply(SpellCastContext context)
    {
        // TODO: Possess target creature when GameState is available
        return new SpellCastResult(true);
    }
}

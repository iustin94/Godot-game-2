namespace DungeonKeeper.Spells.Effects;

/// <summary>
/// Reveals an area of the map around the target tile.
/// </summary>
public class SightOfEvilEffect : ISpellEffect
{
    public SpellCastResult Apply(SpellCastContext context)
    {
        // TODO: Reveal map area when GameState is available
        return new SpellCastResult(true);
    }
}

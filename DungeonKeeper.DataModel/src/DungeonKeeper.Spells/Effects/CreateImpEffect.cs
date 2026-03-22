namespace DungeonKeeper.Spells.Effects;

/// <summary>
/// Spawns an imp at the dungeon heart.
/// </summary>
public class CreateImpEffect : ISpellEffect
{
    public SpellCastResult Apply(SpellCastContext context)
    {
        // TODO: Spawn imp at dungeon heart when GameState is available
        return new SpellCastResult(true);
    }
}

namespace DungeonKeeper.Spells.Effects;

/// <summary>
/// Summons a specific creature type.
/// </summary>
public class SummonCreatureEffect : ISpellEffect
{
    public string CreatureType { get; }

    public SummonCreatureEffect(string creatureType)
    {
        CreatureType = creatureType;
    }

    public SpellCastResult Apply(SpellCastContext context)
    {
        // TODO: Summon creature when GameState is available
        return new SpellCastResult(true);
    }
}

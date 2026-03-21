namespace DungeonKeeper.Spells.Effects;

/// <summary>
/// Applies a buff to the target creature.
/// </summary>
public class BuffCreatureEffect : ISpellEffect
{
    public string BuffType { get; }
    public float Duration { get; }
    public float Magnitude { get; }

    public BuffCreatureEffect(string buffType, float duration, float magnitude)
    {
        BuffType = buffType;
        Duration = duration;
        Magnitude = magnitude;
    }

    public SpellCastResult Apply(SpellCastContext context)
    {
        // TODO: Apply buff to target creature when GameState is available
        return new SpellCastResult(true);
    }
}

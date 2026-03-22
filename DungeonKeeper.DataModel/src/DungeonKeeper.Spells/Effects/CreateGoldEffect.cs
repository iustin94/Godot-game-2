namespace DungeonKeeper.Spells.Effects;

/// <summary>
/// Creates gold at the target location.
/// </summary>
public class CreateGoldEffect : ISpellEffect
{
    public int GoldAmount { get; }

    public CreateGoldEffect(int goldAmount)
    {
        GoldAmount = goldAmount;
    }

    public SpellCastResult Apply(SpellCastContext context)
    {
        // TODO: Create gold when GameState is available
        return new SpellCastResult(true);
    }
}

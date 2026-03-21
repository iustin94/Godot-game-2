namespace DungeonKeeper.Spells.Effects;

public interface ISpellEffect
{
    SpellCastResult Apply(SpellCastContext context);
}

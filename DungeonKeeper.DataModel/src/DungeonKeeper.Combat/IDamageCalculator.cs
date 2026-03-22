namespace DungeonKeeper.Combat;

public interface IDamageCalculator
{
    AttackResult CalculateMelee(
        int attackerAttack, int attackerDamage, int attackerLuck,
        int defenderDefense, int defenderArmor, int defenderHealth);

    AttackResult CalculateRanged(
        int attackerAttack, int attackerDamage,
        int defenderDefense, int defenderArmor, int defenderHealth,
        float range);

    AttackResult CalculateSpell(
        int spellPower, DamageType damageType,
        int defenderArmor, int defenderHealth);
}

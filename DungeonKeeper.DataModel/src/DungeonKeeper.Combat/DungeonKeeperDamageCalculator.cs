namespace DungeonKeeper.Combat;

public class DungeonKeeperDamageCalculator : IDamageCalculator
{
    private readonly Random _random;

    public DungeonKeeperDamageCalculator(Random? random = null)
    {
        _random = random ?? new Random();
    }

    public AttackResult CalculateMelee(
        int attackerAttack, int attackerDamage, int attackerLuck,
        int defenderDefense, int defenderArmor, int defenderHealth)
    {
        // Hit chance: attackerAttack / (attackerAttack + defenderDefense)
        float hitChance = attackerAttack / (float)(attackerAttack + defenderDefense);
        bool hit = _random.NextDouble() < hitChance;

        if (!hit)
            return new AttackResult(false, 0, false, false, DamageType.Physical);

        int damage = Math.Max(1, attackerDamage - defenderArmor / 4);

        // Lucky hit: random check against luck value (0-255), doubles damage
        bool wasCritical = _random.Next(256) < attackerLuck;
        if (wasCritical)
            damage *= 2;

        bool targetKilled = damage >= defenderHealth;

        return new AttackResult(true, damage, wasCritical, targetKilled, DamageType.Physical);
    }

    public AttackResult CalculateRanged(
        int attackerAttack, int attackerDamage,
        int defenderDefense, int defenderArmor, int defenderHealth,
        float range)
    {
        // Hit chance decreases with range
        float hitChance = attackerAttack / (float)(attackerAttack + defenderDefense);
        hitChance *= Math.Max(0.1f, 1.0f - range * 0.05f);
        bool hit = _random.NextDouble() < hitChance;

        if (!hit)
            return new AttackResult(false, 0, false, false, DamageType.Physical);

        int damage = Math.Max(1, attackerDamage - defenderArmor / 4);
        bool targetKilled = damage >= defenderHealth;

        return new AttackResult(true, damage, false, targetKilled, DamageType.Physical);
    }

    public AttackResult CalculateSpell(
        int spellPower, DamageType damageType,
        int defenderArmor, int defenderHealth)
    {
        // Spells always hit; armor is halved against magical damage
        int effectiveArmor = damageType == DamageType.Physical ? defenderArmor : defenderArmor / 2;
        int damage = Math.Max(1, spellPower - effectiveArmor / 4);
        bool targetKilled = damage >= defenderHealth;

        return new AttackResult(true, damage, false, targetKilled, damageType);
    }
}

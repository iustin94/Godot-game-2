using DungeonKeeper.Combat;

namespace DungeonKeeper.Combat.Tests;

public class DamageCalculatorTests
{
    [Fact]
    public void CalculateMelee_ReturnsAnAttackResult()
    {
        var calc = new DungeonKeeperDamageCalculator(new Random(42));

        var result = calc.CalculateMelee(
            attackerAttack: 80, attackerDamage: 15, attackerLuck: 60,
            defenderDefense: 60, defenderArmor: 5, defenderHealth: 100);

        Assert.IsType<AttackResult>(result);
    }

    [Fact]
    public void HigherAttack_HasHigherHitRate()
    {
        // Use a fixed seed and run many trials to test statistical property
        int highAttackHits = 0;
        int lowAttackHits = 0;
        const int trials = 1000;

        var highCalc = new DungeonKeeperDamageCalculator(new Random(123));
        var lowCalc = new DungeonKeeperDamageCalculator(new Random(123));

        for (int i = 0; i < trials; i++)
        {
            var highResult = highCalc.CalculateMelee(
                attackerAttack: 200, attackerDamage: 15, attackerLuck: 0,
                defenderDefense: 50, defenderArmor: 0, defenderHealth: 10000);
            if (highResult.Hit) highAttackHits++;

            var lowResult = lowCalc.CalculateMelee(
                attackerAttack: 10, attackerDamage: 15, attackerLuck: 0,
                defenderDefense: 50, defenderArmor: 0, defenderHealth: 10000);
            if (lowResult.Hit) lowAttackHits++;
        }

        Assert.True(highAttackHits > lowAttackHits,
            $"High attack ({highAttackHits} hits) should hit more often than low attack ({lowAttackHits} hits)");
    }

    [Fact]
    public void Damage_IsReducedByArmor()
    {
        // Use a seed that produces a hit without a critical
        // We try a few seeds to find one that hits and is not critical
        AttackResult noArmorResult = default!;
        AttackResult armorResult = default!;

        for (int seed = 0; seed < 1000; seed++)
        {
            var calc1 = new DungeonKeeperDamageCalculator(new Random(seed));
            var r1 = calc1.CalculateMelee(
                attackerAttack: 100, attackerDamage: 30, attackerLuck: 0,
                defenderDefense: 10, defenderArmor: 0, defenderHealth: 10000);

            var calc2 = new DungeonKeeperDamageCalculator(new Random(seed));
            var r2 = calc2.CalculateMelee(
                attackerAttack: 100, attackerDamage: 30, attackerLuck: 0,
                defenderDefense: 10, defenderArmor: 40, defenderHealth: 10000);

            if (r1.Hit && !r1.WasCritical)
            {
                noArmorResult = r1;
                armorResult = r2;
                break;
            }
        }

        Assert.True(noArmorResult.Hit, "Should find a seed that produces a hit");
        Assert.True(armorResult.DamageDealt <= noArmorResult.DamageDealt,
            $"Armored damage ({armorResult.DamageDealt}) should be <= unarmored damage ({noArmorResult.DamageDealt})");
    }

    [Fact]
    public void ZeroHealthTarget_IsReportedAsKilled()
    {
        // Find a seed that hits
        for (int seed = 0; seed < 1000; seed++)
        {
            var calc = new DungeonKeeperDamageCalculator(new Random(seed));
            var result = calc.CalculateMelee(
                attackerAttack: 100, attackerDamage: 50, attackerLuck: 0,
                defenderDefense: 10, defenderArmor: 0, defenderHealth: 1);

            if (result.Hit)
            {
                Assert.True(result.TargetKilled, "Target with 1 health should be killed when hit for 50 damage");
                return;
            }
        }

        Assert.Fail("Could not find a seed that produces a hit");
    }
}

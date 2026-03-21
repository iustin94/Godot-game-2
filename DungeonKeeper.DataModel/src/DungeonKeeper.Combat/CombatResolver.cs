using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Combat;

public class CombatResolver : ICombatResolver
{
    private readonly IDamageCalculator _damageCalculator;

    public CombatResolver(IDamageCalculator damageCalculator)
    {
        _damageCalculator = damageCalculator;
    }

    public CombatRound ResolveTick(IReadOnlyList<CombatParticipant> participants, GameTime time)
    {
        var attacks = new List<AttackResult>();
        var casualties = new List<EntityId>();
        var fled = new List<EntityId>();
        var paired = new HashSet<int>();

        // Pair up opponents by proximity and different faction (owner)
        for (int i = 0; i < participants.Count; i++)
        {
            if (paired.Contains(i)) continue;

            float bestDistance = float.MaxValue;
            int bestTarget = -1;

            for (int j = i + 1; j < participants.Count; j++)
            {
                if (paired.Contains(j)) continue;
                // Different owner = enemy
                if (participants[i].OwnerId == participants[j].OwnerId) continue;

                float dist = participants[i].Position.DistanceTo(participants[j].Position);
                if (dist < bestDistance)
                {
                    bestDistance = dist;
                    bestTarget = j;
                }
            }

            if (bestTarget < 0) continue;

            paired.Add(i);
            paired.Add(bestTarget);

            // Resolve melee attack between the pair (simplified: use default stats)
            var result = _damageCalculator.CalculateMelee(
                attackerAttack: 10, attackerDamage: 5, attackerLuck: 30,
                defenderDefense: 10, defenderArmor: 2, defenderHealth: 20);

            attacks.Add(result);

            if (result.TargetKilled)
                casualties.Add(participants[bestTarget].CreatureId);
        }

        return new CombatRound(attacks, casualties, fled);
    }
}

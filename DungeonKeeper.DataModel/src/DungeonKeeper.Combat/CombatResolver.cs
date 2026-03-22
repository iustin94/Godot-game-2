using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Creatures.Components;

namespace DungeonKeeper.Combat;

public class CombatResolver : ICombatResolver
{
    private readonly IDamageCalculator _damageCalculator;
    private readonly Func<EntityId, IEntity?>? _entityLookup;

    public CombatResolver(IDamageCalculator damageCalculator, Func<EntityId, IEntity?>? entityLookup = null)
    {
        _damageCalculator = damageCalculator;
        _entityLookup = entityLookup;
    }

    public CombatRound ResolveTick(IReadOnlyList<CombatParticipant> participants, GameTime time)
    {
        var attacks = new List<AttackResult>();
        var casualties = new List<EntityId>();
        var fled = new List<EntityId>();
        var paired = new HashSet<int>();

        for (int i = 0; i < participants.Count; i++)
        {
            if (paired.Contains(i)) continue;

            float bestDistance = float.MaxValue;
            int bestTarget = -1;

            for (int j = i + 1; j < participants.Count; j++)
            {
                if (paired.Contains(j)) continue;
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

            var attackerStats = GetStats(participants[i].CreatureId);
            var defenderStats = GetStats(participants[bestTarget].CreatureId);

            var result = _damageCalculator.CalculateMelee(
                attackerAttack: attackerStats.MeleeAttack,
                attackerDamage: attackerStats.MeleeDamage,
                attackerLuck: attackerStats.Luck,
                defenderDefense: defenderStats.Defense,
                defenderArmor: defenderStats.Armor,
                defenderHealth: defenderStats.CurrentHealth);

            attacks.Add(result);

            // Apply damage to the defender
            if (result.Hit)
            {
                defenderStats.CurrentHealth = Math.Max(0, defenderStats.CurrentHealth - result.DamageDealt);
            }

            if (result.TargetKilled || !defenderStats.IsAlive)
                casualties.Add(participants[bestTarget].CreatureId);
        }

        return new CombatRound(attacks, casualties, fled);
    }

    private StatsComponent GetStats(EntityId entityId)
    {
        var entity = _entityLookup?.Invoke(entityId);
        var stats = entity?.TryGetComponent<StatsComponent>();
        return stats ?? new StatsComponent
        {
            CurrentHealth = 20, MaxHealth = 20,
            MeleeAttack = 10, MeleeDamage = 5,
            Defense = 10, Armor = 2, Luck = 30
        };
    }
}

using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Resources.PayDay;

public class PayDayScheduler
{
    public int TicksBetweenPayDays { get; set; }
    public int TicksUntilNextPayDay { get; set; }

    /// <summary>
    /// Processes a pay day, paying creatures in order from the given resource pool.
    /// Creatures that cannot be paid (insufficient gold) are added to the unpaid list.
    /// </summary>
    public PayDayResult ProcessPayDay(IReadOnlyList<(EntityId CreatureId, int Wage)> creatures, ResourcePool gold)
    {
        var paid = new List<EntityId>();
        var unpaid = new List<EntityId>();
        var totalPaid = 0;

        foreach (var (creatureId, wage) in creatures)
        {
            if (gold.TrySpend(wage))
            {
                paid.Add(creatureId);
                totalPaid += wage;
            }
            else
            {
                unpaid.Add(creatureId);
            }
        }

        return new PayDayResult(totalPaid, paid, unpaid);
    }
}

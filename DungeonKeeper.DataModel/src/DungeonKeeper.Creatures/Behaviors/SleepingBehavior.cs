using DungeonKeeper.Core.Common;
using DungeonKeeper.Creatures.Components;

namespace DungeonKeeper.Creatures.Behaviors;

public sealed class SleepingBehavior : ICreatureBehavior
{
    public void Execute(CreatureBehaviorContext context, GameTime time)
    {
        var needs = context.Entity.TryGetComponent<NeedsComponent>();
        if (needs is not null)
        {
            needs.Tiredness = Math.Max(0f, needs.Tiredness - 0.04f * time.DeltaSeconds);
        }

        var stats = context.Entity.TryGetComponent<StatsComponent>();
        if (stats is not null && stats.IsAlive)
        {
            // Slow health regeneration while sleeping
            stats.CurrentHealth = Math.Min(stats.MaxHealth, stats.CurrentHealth + 1);
        }
    }
}

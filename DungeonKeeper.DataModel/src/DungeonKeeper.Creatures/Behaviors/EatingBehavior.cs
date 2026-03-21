using DungeonKeeper.Core.Common;
using DungeonKeeper.Creatures.Components;

namespace DungeonKeeper.Creatures.Behaviors;

public sealed class EatingBehavior : ICreatureBehavior
{
    public void Execute(CreatureBehaviorContext context, GameTime time)
    {
        var needs = context.Entity.TryGetComponent<NeedsComponent>();
        if (needs is not null)
        {
            needs.Hunger = Math.Max(0f, needs.Hunger - 0.05f * time.DeltaSeconds);
            needs.TicksSinceLastMeal = 0;
        }

        var morale = context.Entity.TryGetComponent<MoraleComponent>();
        if (morale is not null)
        {
            morale.Morale = Math.Min(1f, morale.Morale + 0.005f * time.DeltaSeconds);
        }
    }
}

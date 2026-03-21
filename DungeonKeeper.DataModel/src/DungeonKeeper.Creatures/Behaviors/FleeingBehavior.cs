using DungeonKeeper.Core.Common;
using DungeonKeeper.Creatures.Components;

namespace DungeonKeeper.Creatures.Behaviors;

public sealed class FleeingBehavior : ICreatureBehavior
{
    public void Execute(CreatureBehaviorContext context, GameTime time)
    {
        var needs = context.Entity.TryGetComponent<NeedsComponent>();
        if (needs is not null)
        {
            needs.Tiredness = Math.Min(1f, needs.Tiredness + 0.003f * time.DeltaSeconds);
        }

        var morale = context.Entity.TryGetComponent<MoraleComponent>();
        if (morale is not null)
        {
            morale.Morale = Math.Max(0f, morale.Morale - 0.01f * time.DeltaSeconds);
        }
    }
}

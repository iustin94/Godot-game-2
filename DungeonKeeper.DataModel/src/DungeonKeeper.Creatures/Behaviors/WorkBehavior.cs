using DungeonKeeper.Core.Common;
using DungeonKeeper.Creatures.Components;

namespace DungeonKeeper.Creatures.Behaviors;

public sealed class WorkBehavior : ICreatureBehavior
{
    public void Execute(CreatureBehaviorContext context, GameTime time)
    {
        var needs = context.Entity.TryGetComponent<NeedsComponent>();
        if (needs is not null)
        {
            needs.Hunger = Math.Min(1f, needs.Hunger + 0.002f * time.DeltaSeconds);
            needs.Tiredness = Math.Min(1f, needs.Tiredness + 0.001f * time.DeltaSeconds);
        }

        var xp = context.Entity.TryGetComponent<ExperienceComponent>();
        if (xp is not null)
        {
            xp.CurrentExperience += 1;
            xp.LastSource = ExperienceSource.Work;
        }
    }
}

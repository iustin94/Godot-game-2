using DungeonKeeper.Core.Common;
using DungeonKeeper.Creatures.Components;

namespace DungeonKeeper.Creatures.Behaviors;

public sealed class LeavingBehavior : ICreatureBehavior
{
    public void Execute(CreatureBehaviorContext context, GameTime time)
    {
        var morale = context.Entity.TryGetComponent<MoraleComponent>();
        if (morale is not null)
        {
            morale.State = MoraleState.Leaving;
        }

        // Creature heads toward the portal to leave the dungeon.
        // Movement toward portal is handled by pathfinding externally;
        // this behavior simply ensures the morale state stays at Leaving.
    }
}

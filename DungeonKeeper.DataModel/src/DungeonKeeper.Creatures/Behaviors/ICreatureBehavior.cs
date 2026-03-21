using DungeonKeeper.Core.Common;

namespace DungeonKeeper.Creatures.Behaviors;

public interface ICreatureBehavior
{
    void Execute(CreatureBehaviorContext context, GameTime time);
}

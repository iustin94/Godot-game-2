using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Creatures.Behaviors;

public sealed class CreatureBehaviorContext
{
    public EntityId CreatureId { get; }
    public IEntity Entity { get; }

    public CreatureBehaviorContext(EntityId creatureId, IEntity entity)
    {
        CreatureId = creatureId;
        Entity = entity;
    }
}

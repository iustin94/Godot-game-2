using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Core.Events;

namespace DungeonKeeper.Creatures.Events;

public sealed record CreatureLeveledUpEvent(
    GameTime Timestamp,
    EntityId CreatureId,
    int OldLevel,
    int NewLevel
) : IGameEvent;

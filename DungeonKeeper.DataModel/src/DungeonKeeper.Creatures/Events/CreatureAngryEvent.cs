using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Core.Events;

namespace DungeonKeeper.Creatures.Events;

public sealed record CreatureAngryEvent(
    GameTime Timestamp,
    EntityId CreatureId,
    string Reason
) : IGameEvent;

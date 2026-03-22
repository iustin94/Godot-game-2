using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Core.Events;
using DungeonKeeper.Creatures.Definitions;

namespace DungeonKeeper.Creatures.Events;

public sealed record CreatureDiedEvent(
    GameTime Timestamp,
    EntityId CreatureId,
    CreatureType CreatureType,
    EntityId? KillerId
) : IGameEvent;

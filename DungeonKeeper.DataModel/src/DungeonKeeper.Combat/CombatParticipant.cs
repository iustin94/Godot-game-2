using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Combat;

public record CombatParticipant(
    EntityId CreatureId,
    EntityId OwnerId,
    TileCoordinate Position
);

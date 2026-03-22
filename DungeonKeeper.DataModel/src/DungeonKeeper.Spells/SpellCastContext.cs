using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Spells;

public record SpellCastContext(
    EntityId CasterId,
    TileCoordinate? TargetTile,
    EntityId? TargetCreature);

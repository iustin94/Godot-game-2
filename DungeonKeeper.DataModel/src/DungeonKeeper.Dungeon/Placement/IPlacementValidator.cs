using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Dungeon.Map;

namespace DungeonKeeper.Dungeon.Placement;

public interface IPlacementValidator
{
    bool CanPlace(DungeonMap map, TileCoordinate coordinate, EntityId owner);
}

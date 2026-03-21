using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Dungeon.Map;

namespace DungeonKeeper.Dungeon.Placement;

public class RoomPlacementValidator : IPlacementValidator
{
    public bool CanPlace(DungeonMap map, TileCoordinate coordinate, EntityId owner)
    {
        var tile = map.GetTile(coordinate);
        if (tile == null) return false;

        // Tile must be claimed path owned by the placing player
        if (tile.Type != TileType.ClaimedPath) return false;
        if (tile.OwnerId != owner) return false;

        // Tile must not already have a room
        if (tile.RoomInstanceId != null) return false;

        return true;
    }
}

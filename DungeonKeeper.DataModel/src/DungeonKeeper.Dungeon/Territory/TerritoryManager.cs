using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Dungeon.Map;

namespace DungeonKeeper.Dungeon.Territory;

public class TerritoryManager
{
    private readonly DungeonMap _map;

    public TerritoryManager(DungeonMap map)
    {
        _map = map;
    }

    public bool ClaimTile(TileCoordinate coord, EntityId ownerId)
    {
        var tile = _map.GetTile(coord);
        if (tile == null) return false;
        if (tile.Type is TileType.Impenetrable or TileType.Lava or TileType.Water) return false;

        tile.OwnerId = ownerId;
        if (tile.Type == TileType.Earth)
            tile.Type = TileType.ClaimedPath;

        return true;
    }

    public bool UnclaimTile(TileCoordinate coord)
    {
        var tile = _map.GetTile(coord);
        if (tile == null) return false;
        if (tile.OwnerId == null) return false;

        tile.OwnerId = null;
        return true;
    }

    public int GetOwnedTileCount(EntityId ownerId)
    {
        return _map.GetOwnedTiles(ownerId).Count();
    }
}

using DungeonKeeper.Core.Common;

namespace DungeonKeeper.Dungeon.Map;

public interface IPathfinding
{
    IReadOnlyList<TileCoordinate>? FindPath(TileCoordinate from, TileCoordinate to);
}

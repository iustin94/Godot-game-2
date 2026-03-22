using DungeonKeeper.Core.Common;

namespace DungeonKeeper.Dungeon.Map;

public class AStarPathfinding : IPathfinding
{
    private readonly DungeonMap _map;

    public AStarPathfinding(DungeonMap map)
    {
        _map = map;
    }

    public IReadOnlyList<TileCoordinate>? FindPath(TileCoordinate from, TileCoordinate to)
    {
        if (from == to) return Array.Empty<TileCoordinate>();

        var openSet = new PriorityQueue<TileCoordinate, float>();
        var cameFrom = new Dictionary<TileCoordinate, TileCoordinate>();
        var gScore = new Dictionary<TileCoordinate, float> { [from] = 0 };

        openSet.Enqueue(from, Heuristic(from, to));

        while (openSet.Count > 0)
        {
            var current = openSet.Dequeue();

            if (current == to)
                return ReconstructPath(cameFrom, current);

            foreach (var neighbor in current.GetNeighbors())
            {
                if (!IsWalkable(neighbor, to)) continue;

                float tentativeG = gScore[current] + 1f;

                if (tentativeG < gScore.GetValueOrDefault(neighbor, float.MaxValue))
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeG;
                    float fScore = tentativeG + Heuristic(neighbor, to);
                    openSet.Enqueue(neighbor, fScore);
                }
            }
        }

        return null; // No path found
    }

    /// <summary>
    /// Find a path to any tile adjacent to the target (for digging — target itself isn't passable).
    /// </summary>
    public IReadOnlyList<TileCoordinate>? FindPathAdjacentTo(TileCoordinate from, TileCoordinate target)
    {
        IReadOnlyList<TileCoordinate>? bestPath = null;

        foreach (var neighbor in target.GetNeighbors())
        {
            if (!_map.IsPassable(neighbor)) continue;
            if (neighbor == from) return new[] { from };

            var path = FindPath(from, neighbor);
            if (path != null && (bestPath == null || path.Count < bestPath.Count))
                bestPath = path;
        }

        return bestPath;
    }

    private bool IsWalkable(TileCoordinate coord, TileCoordinate destination)
    {
        if (coord == destination) return _map.IsPassable(coord);

        var tile = _map.GetTile(coord);
        if (tile == null) return false;
        return _map.IsPassable(coord);
    }

    private static float Heuristic(TileCoordinate a, TileCoordinate b)
    {
        return a.ManhattanDistanceTo(b);
    }

    private static IReadOnlyList<TileCoordinate> ReconstructPath(
        Dictionary<TileCoordinate, TileCoordinate> cameFrom, TileCoordinate current)
    {
        var path = new List<TileCoordinate> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }
        path.Reverse();
        return path;
    }
}

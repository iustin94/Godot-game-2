using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Dungeon.Rooms;

namespace DungeonKeeper.Dungeon.Map;

public class DungeonMap
{
    private readonly Tile[,] _tiles;

    public int Width { get; }
    public int Height { get; }

    public DungeonMap(int width, int height)
    {
        Width = width;
        Height = height;
        _tiles = new Tile[width, height];

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                _tiles[x, y] = new Tile
                {
                    Coordinate = new TileCoordinate(x, y),
                    Type = TileType.Earth,
                    Health = 100f
                };
            }
        }
    }

    public Tile? GetTile(TileCoordinate coord)
    {
        if (coord.X < 0 || coord.X >= Width || coord.Y < 0 || coord.Y >= Height)
            return null;
        return _tiles[coord.X, coord.Y];
    }

    public IEnumerable<Tile> GetTilesInRect(TileCoordinate topLeft, TileCoordinate bottomRight)
    {
        var minX = Math.Max(0, Math.Min(topLeft.X, bottomRight.X));
        var maxX = Math.Min(Width - 1, Math.Max(topLeft.X, bottomRight.X));
        var minY = Math.Max(0, Math.Min(topLeft.Y, bottomRight.Y));
        var maxY = Math.Min(Height - 1, Math.Max(topLeft.Y, bottomRight.Y));

        for (var x = minX; x <= maxX; x++)
        {
            for (var y = minY; y <= maxY; y++)
            {
                yield return _tiles[x, y];
            }
        }
    }

    public IEnumerable<Tile> GetTilesForRoom(EntityId roomInstanceId)
    {
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                if (_tiles[x, y].RoomInstanceId == roomInstanceId)
                    yield return _tiles[x, y];
            }
        }
    }

    public IEnumerable<Tile> GetOwnedTiles(EntityId ownerId)
    {
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                if (_tiles[x, y].OwnerId == ownerId)
                    yield return _tiles[x, y];
            }
        }
    }

    public bool IsDiggable(TileCoordinate coord)
    {
        var tile = GetTile(coord);
        if (tile == null) return false;
        return tile.Type is TileType.Earth or TileType.Gold or TileType.Gem;
    }

    public bool IsPassable(TileCoordinate coord)
    {
        var tile = GetTile(coord);
        if (tile == null) return false;
        return tile.Type is TileType.ClaimedPath or TileType.Room;
    }
}

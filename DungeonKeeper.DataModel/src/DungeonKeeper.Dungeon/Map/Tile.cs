using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Dungeon.Rooms;

namespace DungeonKeeper.Dungeon.Map;

public class Tile
{
    public TileCoordinate Coordinate { get; init; }
    public TileType Type { get; set; }
    public EntityId? OwnerId { get; set; }
    public RoomType? RoomType { get; set; }
    public EntityId? RoomInstanceId { get; set; }
    public int GoldRemaining { get; set; }
    public float Health { get; set; }
    public bool IsRevealed { get; set; }
    public bool IsMarkedForDigging { get; set; }
}

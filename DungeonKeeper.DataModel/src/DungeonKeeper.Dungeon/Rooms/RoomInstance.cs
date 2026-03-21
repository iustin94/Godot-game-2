using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Dungeon.Rooms;

public class RoomInstance
{
    public EntityId Id { get; init; }
    public RoomType Type { get; init; }
    public EntityId OwnerId { get; set; }
    public List<TileCoordinate> Tiles { get; } = new();
    public int TileCount => Tiles.Count;
    public int EffectiveLevel { get; set; } = 1;
    public float Health { get; set; }
    public int MinimumSize { get; init; }
    public bool IsOperational => TileCount >= MinimumSize;
    public List<EntityId> AssignedCreatures { get; } = new();
    public int Capacity { get; set; }
}

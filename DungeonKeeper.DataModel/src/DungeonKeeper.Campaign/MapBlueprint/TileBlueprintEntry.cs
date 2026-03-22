using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Dungeon.Map;
using DungeonKeeper.Dungeon.Rooms;

namespace DungeonKeeper.Campaign.MapBlueprint;

public class TileBlueprintEntry
{
    public required TileCoordinate Coordinate { get; init; }
    public required TileType Type { get; init; }
    public EntityId? OwnerId { get; init; }
    public RoomType? RoomType { get; init; }
    public int GoldRemaining { get; init; }
    public bool IsRevealed { get; init; }
}

using DungeonKeeper.Dungeon.Rooms.RoomEffects;

namespace DungeonKeeper.Dungeon.Rooms;

public class RoomDefinition
{
    public required RoomType Type { get; init; }
    public required string Name { get; init; }
    public required string AssetId { get; init; }
    public int GoldCostPerTile { get; init; }
    public int MinimumSize { get; init; }
    public bool RequiresClaimedFloor { get; init; } = true;
    public bool CanBuildOnWater { get; init; }
    public bool CanBuildOnLava { get; init; }
    public int? MaxPerDungeon { get; init; }
    public IReadOnlyList<IRoomEffect> Effects { get; init; } = Array.Empty<IRoomEffect>();
    public bool AvailableByDefault { get; init; }
    public int? ResearchPointsToUnlock { get; init; }
}

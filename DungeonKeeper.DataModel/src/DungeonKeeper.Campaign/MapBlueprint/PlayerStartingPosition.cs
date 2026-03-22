using DungeonKeeper.Core.Common;

namespace DungeonKeeper.Campaign.MapBlueprint;

public class PlayerStartingPosition
{
    public required TileCoordinate DungeonHeartCenter { get; init; }
    public int ClaimedAreaRadius { get; init; } = 2;
}

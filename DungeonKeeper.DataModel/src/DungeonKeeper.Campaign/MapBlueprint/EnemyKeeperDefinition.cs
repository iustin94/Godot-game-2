using DungeonKeeper.Campaign.Availability;
using DungeonKeeper.Core.Common;
using DungeonKeeper.Creatures.Definitions;

namespace DungeonKeeper.Campaign.MapBlueprint;

public class EnemyKeeperDefinition
{
    public required string KeeperId { get; init; }
    public required string Name { get; init; }
    public required TileCoordinate DungeonHeartCenter { get; init; }
    public int StartingGold { get; init; } = 10000;
    public IReadOnlyList<CreatureType> StartingCreatures { get; init; } = Array.Empty<CreatureType>();
    public LevelAvailability? Availability { get; init; }
}

using DungeonKeeper.Core.Common;

namespace DungeonKeeper.Campaign.MapBlueprint;

public class HeroGateDefinition
{
    public required string GateId { get; init; }
    public required TileCoordinate Location { get; init; }
}

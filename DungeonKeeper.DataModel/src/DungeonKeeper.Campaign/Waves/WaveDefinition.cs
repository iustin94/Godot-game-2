using DungeonKeeper.Keeper.HeroInvasion;

namespace DungeonKeeper.Campaign.Waves;

public class WaveDefinition
{
    public required int WaveNumber { get; init; }
    public required int ScheduledTick { get; init; }
    public required string SourceGateId { get; init; }
    public required IReadOnlyList<InvasionGroup> Groups { get; init; }
}

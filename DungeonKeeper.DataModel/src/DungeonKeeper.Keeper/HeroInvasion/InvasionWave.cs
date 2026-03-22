using DungeonKeeper.Core.Common;

namespace DungeonKeeper.Keeper.HeroInvasion;

public class InvasionWave
{
    public int WaveNumber { get; init; }
    public IReadOnlyList<InvasionGroup> Groups { get; init; } = Array.Empty<InvasionGroup>();
    public TileCoordinate EntryPoint { get; init; }
    public int ScheduledTick { get; init; }
}

namespace DungeonKeeper.Keeper.HeroInvasion;

public class InvasionScheduler
{
    private readonly List<InvasionWave> _scheduledWaves = new();

    public IReadOnlyList<InvasionWave> ScheduledWaves => _scheduledWaves;

    public void ScheduleWave(InvasionWave wave)
    {
        _scheduledWaves.Add(wave);
        _scheduledWaves.Sort((a, b) => a.ScheduledTick.CompareTo(b.ScheduledTick));
    }

    public InvasionWave? GetWaveForTick(int tick)
    {
        return _scheduledWaves.FirstOrDefault(w => w.ScheduledTick == tick);
    }

    public IReadOnlyList<InvasionWave> GetUpcomingWaves(int currentTick)
    {
        return _scheduledWaves
            .Where(w => w.ScheduledTick > currentTick)
            .ToList()
            .AsReadOnly();
    }
}

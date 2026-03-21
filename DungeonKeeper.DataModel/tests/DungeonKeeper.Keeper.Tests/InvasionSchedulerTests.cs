using DungeonKeeper.Core.Common;
using DungeonKeeper.Keeper.HeroInvasion;

namespace DungeonKeeper.Keeper.Tests;

public class InvasionSchedulerTests
{
    private static InvasionWave CreateWave(int waveNumber, int scheduledTick) => new()
    {
        WaveNumber = waveNumber,
        ScheduledTick = scheduledTick,
        EntryPoint = new TileCoordinate(0, 0)
    };

    [Fact]
    public void Can_schedule_a_wave()
    {
        var scheduler = new InvasionScheduler();
        var wave = CreateWave(1, 100);

        scheduler.ScheduleWave(wave);

        Assert.Single(scheduler.ScheduledWaves);
        Assert.Same(wave, scheduler.ScheduledWaves[0]);
    }

    [Fact]
    public void GetWaveForTick_returns_correct_wave()
    {
        var scheduler = new InvasionScheduler();
        var wave1 = CreateWave(1, 100);
        var wave2 = CreateWave(2, 200);
        scheduler.ScheduleWave(wave1);
        scheduler.ScheduleWave(wave2);

        var result = scheduler.GetWaveForTick(200);

        Assert.NotNull(result);
        Assert.Same(wave2, result);
    }

    [Fact]
    public void Returns_null_for_tick_with_no_wave()
    {
        var scheduler = new InvasionScheduler();
        scheduler.ScheduleWave(CreateWave(1, 100));

        var result = scheduler.GetWaveForTick(50);

        Assert.Null(result);
    }
}

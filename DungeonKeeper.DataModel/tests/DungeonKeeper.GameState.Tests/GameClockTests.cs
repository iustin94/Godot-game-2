using DungeonKeeper.GameState;

namespace DungeonKeeper.GameState.Tests;

public class GameClockTests
{
    [Fact]
    public void Advance_increments_tick()
    {
        var clock = new GameClock();
        Assert.Equal(0, clock.CurrentTick);

        clock.Advance();

        Assert.Equal(1, clock.CurrentTick);
    }

    [Fact]
    public void TotalElapsedSeconds_calculated_correctly()
    {
        var clock = new GameClock(); // default TickDurationSeconds = 0.1f
        clock.Advance();
        clock.Advance();
        clock.Advance();

        Assert.Equal(3 * 0.1f, clock.TotalElapsedSeconds);
    }

    [Fact]
    public void Current_returns_correct_GameTime()
    {
        var clock = new GameClock();
        clock.Advance();
        clock.Advance();

        var gt = clock.Current;

        Assert.Equal(2, gt.TickNumber);
        Assert.Equal(0.1f, gt.DeltaSeconds);
        Assert.Equal(2 * 0.1f, gt.TotalSeconds);
    }
}

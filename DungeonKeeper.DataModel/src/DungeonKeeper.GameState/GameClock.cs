using DungeonKeeper.Core.Common;

namespace DungeonKeeper.GameState;

public class GameClock
{
    public int CurrentTick { get; private set; }
    public float TickDurationSeconds { get; init; } = 0.1f;
    public float TotalElapsedSeconds => CurrentTick * TickDurationSeconds;

    public GameTime Advance()
    {
        CurrentTick++;
        return new GameTime(CurrentTick, TickDurationSeconds, TotalElapsedSeconds);
    }

    public GameTime Current => new(CurrentTick, TickDurationSeconds, TotalElapsedSeconds);
}

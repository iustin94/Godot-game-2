using DungeonKeeper.GameState;

namespace DungeonKeeper.Campaign.Conditions;

public class TimeLimitDefeatCondition : IDefeatCondition
{
    public float TimeLimitSeconds { get; init; }

    public string Description => $"Time limit: {TimeLimitSeconds:F0} seconds";

    public bool IsMet(GameSession session)
    {
        return session.Clock.TotalElapsedSeconds >= TimeLimitSeconds;
    }
}

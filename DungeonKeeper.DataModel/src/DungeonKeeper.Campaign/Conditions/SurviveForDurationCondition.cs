using DungeonKeeper.GameState;

namespace DungeonKeeper.Campaign.Conditions;

public class SurviveForDurationCondition : IVictoryCondition
{
    public float RequiredSeconds { get; init; }

    public string Description => $"Survive for {RequiredSeconds:F0} seconds";

    public bool IsMet(GameSession session)
    {
        return session.Clock.TotalElapsedSeconds >= RequiredSeconds;
    }
}

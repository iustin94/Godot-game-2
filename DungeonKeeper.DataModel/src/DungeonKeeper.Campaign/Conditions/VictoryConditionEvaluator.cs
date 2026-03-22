using DungeonKeeper.GameState;

namespace DungeonKeeper.Campaign.Conditions;

public class VictoryConditionEvaluator
{
    private readonly LevelDefinition _level;

    public VictoryConditionEvaluator(LevelDefinition level)
    {
        _level = level;
    }

    public LevelOutcome Evaluate(GameSession session)
    {
        foreach (var condition in _level.DefeatConditions)
        {
            if (condition.IsMet(session))
                return LevelOutcome.Defeat;
        }

        bool allMet = _level.VictoryConditions.All(c => c.IsMet(session));
        return allMet ? LevelOutcome.Victory : LevelOutcome.InProgress;
    }
}

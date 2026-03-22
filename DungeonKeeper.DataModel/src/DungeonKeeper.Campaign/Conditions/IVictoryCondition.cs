using DungeonKeeper.GameState;

namespace DungeonKeeper.Campaign.Conditions;

public interface IVictoryCondition
{
    string Description { get; }
    bool IsMet(GameSession session);
}

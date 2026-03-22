using DungeonKeeper.GameState;

namespace DungeonKeeper.Campaign.Conditions;

public interface IDefeatCondition
{
    string Description { get; }
    bool IsMet(GameSession session);
}

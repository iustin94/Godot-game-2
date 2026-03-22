using DungeonKeeper.GameState;

namespace DungeonKeeper.Campaign.Conditions;

public class AccumulateGoldCondition : IVictoryCondition
{
    public int RequiredGold { get; init; }

    public string Description => $"Accumulate {RequiredGold} gold";

    public bool IsMet(GameSession session)
    {
        if (session.Players.Count == 0) return false;
        return session.Players[0].Dungeon.Gold.Current >= RequiredGold;
    }
}

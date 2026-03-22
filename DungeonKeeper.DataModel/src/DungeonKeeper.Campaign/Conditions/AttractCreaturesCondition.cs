using DungeonKeeper.GameState;

namespace DungeonKeeper.Campaign.Conditions;

public class AttractCreaturesCondition : IVictoryCondition
{
    public int RequiredCount { get; init; }

    public string Description => $"Attract {RequiredCount} creatures to your dungeon";

    public bool IsMet(GameSession session)
    {
        if (session.Players.Count == 0) return false;
        var player = session.Players[0];
        // Subtract imps from count — only count attracted creatures
        return player.Dungeon.OwnedCreatureIds.Count >= RequiredCount;
    }
}

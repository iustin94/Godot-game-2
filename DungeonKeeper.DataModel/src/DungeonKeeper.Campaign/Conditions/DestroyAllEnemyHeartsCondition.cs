using DungeonKeeper.Dungeon.Rooms;
using DungeonKeeper.GameState;

namespace DungeonKeeper.Campaign.Conditions;

public class DestroyAllEnemyHeartsCondition : IVictoryCondition
{
    public string Description => "Destroy all enemy dungeon hearts";

    public bool IsMet(GameSession session)
    {
        if (session.Players.Count <= 1) return false;

        for (int i = 1; i < session.Players.Count; i++)
        {
            var enemyPlayer = session.Players[i];
            var hearts = enemyPlayer.Dungeon.OwnedRooms
                .Where(r => r.Type == RoomType.DungeonHeart);

            foreach (var heart in hearts)
            {
                if (heart.Health > 0) return false;
            }
        }

        return true;
    }
}

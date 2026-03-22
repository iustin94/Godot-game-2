using DungeonKeeper.Dungeon.Rooms;
using DungeonKeeper.GameState;

namespace DungeonKeeper.Campaign.Conditions;

public class DungeonHeartDestroyedCondition : IDefeatCondition
{
    public string Description => "Your dungeon heart has been destroyed";

    public bool IsMet(GameSession session)
    {
        if (session.Players.Count == 0) return false;

        var player = session.Players[0];
        var hearts = player.Dungeon.OwnedRooms
            .Where(r => r.Type == RoomType.DungeonHeart);

        foreach (var heart in hearts)
        {
            if (heart.Health <= 0) return true;
        }

        return false;
    }
}

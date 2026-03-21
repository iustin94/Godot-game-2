using DungeonKeeper.Creatures.Definitions;
using DungeonKeeper.Dungeon.Rooms;

namespace DungeonKeeper.Creatures.AI;

public static class JobAssignment
{
    /// <summary>
    /// Assigns a creature to the best available room job based on its definition's job preferences.
    /// Returns the preferred RoomType if one is available, or null if no suitable job exists.
    /// </summary>
    public static RoomType? AssignBestJob(
        CreatureDefinition definition,
        IReadOnlySet<RoomType> availableRooms)
    {
        foreach (var preferred in definition.JobPreferences)
        {
            if (availableRooms.Contains(preferred))
                return preferred;
        }

        return null;
    }
}

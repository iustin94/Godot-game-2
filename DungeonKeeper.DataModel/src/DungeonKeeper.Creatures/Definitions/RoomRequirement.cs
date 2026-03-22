using DungeonKeeper.Dungeon.Rooms;

namespace DungeonKeeper.Creatures.Definitions;

public record RoomRequirement(RoomType RoomType, int MinimumSize);

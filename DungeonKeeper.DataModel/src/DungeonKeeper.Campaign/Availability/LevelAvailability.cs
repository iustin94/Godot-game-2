using DungeonKeeper.Creatures.Definitions;
using DungeonKeeper.Dungeon.Rooms;
using DungeonKeeper.Spells;
using DungeonKeeper.Traps;

namespace DungeonKeeper.Campaign.Availability;

public class LevelAvailability
{
    public IReadOnlyList<RoomType> AvailableRooms { get; init; } = Array.Empty<RoomType>();
    public IReadOnlyList<SpellType> AvailableSpells { get; init; } = Array.Empty<SpellType>();
    public IReadOnlyList<CreatureType> AvailableCreatures { get; init; } = Array.Empty<CreatureType>();
    public IReadOnlyList<TrapType> AvailableTraps { get; init; } = Array.Empty<TrapType>();
    public IReadOnlyList<DoorType> AvailableDoors { get; init; } = Array.Empty<DoorType>();
}

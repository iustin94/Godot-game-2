using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Dungeon.Rooms;
using DungeonKeeper.Keeper.HandOfEvil;
using DungeonKeeper.Keeper.HeroInvasion;
using DungeonKeeper.Keeper.Possession;
using DungeonKeeper.Resources;
using DungeonKeeper.Resources.PayDay;
using DungeonKeeper.Spells;
using DungeonKeeper.Traps.Manufacturing;

namespace DungeonKeeper.GameState;

public class PlayerDungeon
{
    public EntityId OwnerId { get; init; }
    public ResourcePool Gold { get; init; } = new() { Type = ResourceType.Gold };
    public ManaPool Mana { get; init; } = new();
    public Treasury Treasury { get; init; } = new();
    public ResearchTree ResearchTree { get; init; } = null!;
    public ManufacturingQueue ManufacturingQueue { get; init; } = new();
    public HandInventory HandInventory { get; init; } = new();
    public PossessionSession? ActivePossession { get; set; }
    public PayDayScheduler PayDayScheduler { get; init; } = new();
    public InvasionScheduler InvasionScheduler { get; init; } = new();

    private readonly List<EntityId> _ownedCreatureIds = new();
    public IReadOnlyList<EntityId> OwnedCreatureIds => _ownedCreatureIds;

    private readonly List<RoomInstance> _ownedRooms = new();
    public IReadOnlyList<RoomInstance> OwnedRooms => _ownedRooms;

    private readonly List<EntityId> _ownedTrapIds = new();
    public IReadOnlyList<EntityId> OwnedTrapIds => _ownedTrapIds;

    private readonly List<EntityId> _ownedDoorIds = new();
    public IReadOnlyList<EntityId> OwnedDoorIds => _ownedDoorIds;

    public bool HasRoom(RoomType roomType, int minSize = 1)
    {
        return _ownedRooms.Any(r => r.Type == roomType && r.TileCount >= minSize);
    }

    public IReadOnlyList<RoomInstance> GetRooms(RoomType roomType)
    {
        return _ownedRooms.Where(r => r.Type == roomType).ToList().AsReadOnly();
    }

    public void AddCreature(EntityId creatureId)
    {
        if (!_ownedCreatureIds.Contains(creatureId))
            _ownedCreatureIds.Add(creatureId);
    }

    public bool RemoveCreature(EntityId creatureId)
    {
        return _ownedCreatureIds.Remove(creatureId);
    }

    public void AddRoom(RoomInstance room)
    {
        _ownedRooms.Add(room);
    }

    public void AddTrap(EntityId trapId)
    {
        _ownedTrapIds.Add(trapId);
    }

    public void AddDoor(EntityId doorId)
    {
        _ownedDoorIds.Add(doorId);
    }
}

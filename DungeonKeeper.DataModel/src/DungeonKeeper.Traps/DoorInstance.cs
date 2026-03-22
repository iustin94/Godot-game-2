using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Traps;

public sealed class DoorInstance
{
    public required EntityId Id { get; init; }
    public required string DoorDefinitionId { get; init; }
    public required TileCoordinate Position { get; init; }
    public required EntityId OwnerId { get; init; }
    public float CurrentHealth { get; set; }
    public bool IsOpen { get; set; }
    public bool IsLocked { get; set; }
}

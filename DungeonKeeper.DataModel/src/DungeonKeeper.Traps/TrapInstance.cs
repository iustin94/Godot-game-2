using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Traps;

public sealed class TrapInstance
{
    public required EntityId Id { get; init; }
    public required string TrapDefinitionId { get; init; }
    public required TileCoordinate Position { get; init; }
    public required EntityId OwnerId { get; init; }
    public TrapState State { get; set; } = TrapState.Armed;
    public float Health { get; set; }
}

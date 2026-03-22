using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Creatures.Components;

public sealed class MovementComponent : IComponent
{
    public TileCoordinate CurrentPosition { get; set; }
    public TileCoordinate? TargetPosition { get; set; }
    public List<TileCoordinate> CurrentPath { get; init; } = new();
    public bool IsFlying { get; set; }
}

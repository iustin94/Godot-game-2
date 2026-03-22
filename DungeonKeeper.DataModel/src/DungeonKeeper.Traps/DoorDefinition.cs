namespace DungeonKeeper.Traps;

public sealed class DoorDefinition
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string AssetId { get; init; }
    public required DoorType Type { get; init; }
    public required int ManufactureCost { get; init; }
    public required int ManufactureTime { get; init; }
    public required int Health { get; init; }
    public required bool IsLocked { get; init; }
    public required bool IsSecret { get; init; }
    public required bool IsMagic { get; init; }
}

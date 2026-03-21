namespace DungeonKeeper.Traps.Manufacturing;

public sealed class BlueprintDefinition
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required BlueprintCategory Category { get; init; }
    public required int ManufactureTime { get; init; }
    public required int GoldCost { get; init; }
    public bool IsResearched { get; set; }
}

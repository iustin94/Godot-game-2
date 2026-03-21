namespace DungeonKeeper.Traps.Manufacturing;

public sealed class ManufacturingJob
{
    public required BlueprintDefinition Blueprint { get; init; }
    public int ProgressPoints { get; set; }
    public bool IsComplete => ProgressPoints >= Blueprint.ManufactureTime;
}

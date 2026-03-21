using DungeonKeeper.Combat;

namespace DungeonKeeper.Traps;

public sealed class TrapDefinition
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string AssetId { get; init; }
    public required TrapType Type { get; init; }
    public required int ManufactureCost { get; init; }
    public required int ManufactureTime { get; init; }
    public required int Damage { get; init; }
    public required DamageType DamageType { get; init; }
    public required float TriggerRadius { get; init; }
    public required TrapTriggerType TriggerType { get; init; }
    public required bool IsReusable { get; init; }
    public required float RearmTime { get; init; }
    public IReadOnlyList<string> AppliedEffects { get; init; } = [];
}

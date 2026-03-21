namespace DungeonKeeper.Combat;

public record AttackResult(
    bool Hit,
    int DamageDealt,
    bool WasCritical,
    bool TargetKilled,
    DamageType DamageType
);

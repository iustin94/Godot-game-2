namespace DungeonKeeper.Creatures.Definitions;

public record LevelProgression(
    int HealthPerLevel,
    int AttackPerLevel,
    int DamagePerLevel,
    int DefensePerLevel,
    int ArmorPerLevel,
    float SpeedPerLevel
);

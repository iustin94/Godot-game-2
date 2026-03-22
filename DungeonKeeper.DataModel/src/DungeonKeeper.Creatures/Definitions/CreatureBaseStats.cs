namespace DungeonKeeper.Creatures.Definitions;

public record CreatureBaseStats(
    int MaxHealth,
    int MeleeAttack,
    int MeleeDamage,
    int Defense,
    int Armor,
    int Luck,
    float Speed,
    int ResearchSkill,
    int ManufactureSkill,
    int TrainingCost
);

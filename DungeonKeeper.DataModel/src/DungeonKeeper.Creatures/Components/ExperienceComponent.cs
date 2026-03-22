using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Creatures.Components;

public enum ExperienceSource
{
    Combat,
    Training,
    CombatPit,
    Work
}

public sealed class ExperienceComponent : IComponent
{
    public int CurrentExperience { get; set; }
    public int ExperienceToNextLevel { get; set; }
    public ExperienceSource LastSource { get; set; }
}

using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Creatures.Components;

public sealed class StatsComponent : IComponent
{
    private int _level = 1;

    public int Level
    {
        get => _level;
        set => _level = Math.Clamp(value, 1, 10);
    }

    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }
    public int MeleeAttack { get; set; }
    public int MeleeDamage { get; set; }
    public int Defense { get; set; }
    public int Armor { get; set; }
    public int Luck { get; set; }
    public float Speed { get; set; }

    public bool IsAlive => CurrentHealth > 0;
}

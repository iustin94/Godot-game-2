using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Creatures.Components;

public sealed class NeedsComponent : IComponent
{
    public float Hunger { get; set; }
    public float Tiredness { get; set; }
    public float PaySatisfaction { get; set; } = 1f;
    public float Happiness { get; set; } = 1f;
    public bool HasLair { get; set; }
    public int TicksSinceLastPaid { get; set; }
    public int TicksSinceLastMeal { get; set; }
}

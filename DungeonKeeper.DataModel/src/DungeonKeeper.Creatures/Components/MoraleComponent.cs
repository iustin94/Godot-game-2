using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Creatures.Components;

public enum MoraleState
{
    Content,
    Unhappy,
    Angry,
    Leaving
}

public sealed class MoraleComponent : IComponent
{
    public float Morale { get; set; } = 1f;
    public MoraleState State { get; set; } = MoraleState.Content;
    public int AngerTicks { get; set; }
}

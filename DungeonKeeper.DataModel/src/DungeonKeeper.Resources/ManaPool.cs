using DungeonKeeper.Core.Common;

namespace DungeonKeeper.Resources;

public class ManaPool : ResourcePool
{
    public ManaPool()
    {
        Type = ResourceType.Mana;
    }

    /// <summary>
    /// Mana drain rate from imps and other ongoing costs (mana per second).
    /// </summary>
    public float DrainRate { get; set; }

    /// <summary>
    /// Net mana generation rate after subtracting drain.
    /// </summary>
    public float NetRate => GenerationRate - DrainRate;
}

using DungeonKeeper.Core.Common;

namespace DungeonKeeper.Resources;

public class ResourcePool
{
    public ResourceType Type { get; init; }
    public int Current { get; set; }
    public int Capacity { get; set; }
    public float GenerationRate { get; set; }

    public bool CanAfford(int amount) => Current >= amount;

    public bool TrySpend(int amount)
    {
        if (!CanAfford(amount)) return false;
        Current -= amount;
        return true;
    }

    public void Add(int amount)
    {
        Current = Math.Min(Current + amount, Capacity);
    }
}

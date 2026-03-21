namespace DungeonKeeper.Core.Common;

public readonly record struct ResourceAmount(ResourceType Type, int Value)
{
    public static ResourceAmount Gold(int value) => new(ResourceType.Gold, value);
    public static ResourceAmount Mana(int value) => new(ResourceType.Mana, value);

    public override string ToString() => $"{Value} {Type}";
}

public enum ResourceType
{
    Gold,
    Mana
}

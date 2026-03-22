namespace GameCore.Common;

public readonly record struct ResourceAmount(string Type, int Value)
{
    public override string ToString() => $"{Value} {Type}";
}

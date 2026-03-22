namespace DungeonKeeper.Core.Common;

public readonly record struct Percentage
{
    public float Value { get; }

    public Percentage(float value) =>
        Value = Math.Clamp(value, 0f, 1f);

    public static Percentage Full => new(1f);
    public static Percentage Zero => new(0f);
    public static Percentage FromPercent(int percent) => new(percent / 100f);

    public override string ToString() => $"{Value * 100:F0}%";
}

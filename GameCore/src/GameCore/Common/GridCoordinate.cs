namespace GameCore.Common;

public readonly record struct GridCoordinate(int X, int Y)
{
    public IEnumerable<GridCoordinate> GetNeighbors() => new[]
    {
        new GridCoordinate(X - 1, Y),
        new GridCoordinate(X + 1, Y),
        new GridCoordinate(X, Y - 1),
        new GridCoordinate(X, Y + 1)
    };

    public float DistanceTo(GridCoordinate other)
    {
        var dx = X - other.X;
        var dy = Y - other.Y;
        return MathF.Sqrt(dx * dx + dy * dy);
    }

    public int ManhattanDistanceTo(GridCoordinate other)
    {
        return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
    }

    public override string ToString() => $"({X}, {Y})";
}

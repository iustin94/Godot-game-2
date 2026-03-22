namespace DungeonKeeper.Core.Common;

public readonly record struct TileCoordinate(int X, int Y)
{
    public IEnumerable<TileCoordinate> GetNeighbors() => new[]
    {
        new TileCoordinate(X - 1, Y),
        new TileCoordinate(X + 1, Y),
        new TileCoordinate(X, Y - 1),
        new TileCoordinate(X, Y + 1)
    };

    public float DistanceTo(TileCoordinate other)
    {
        var dx = X - other.X;
        var dy = Y - other.Y;
        return MathF.Sqrt(dx * dx + dy * dy);
    }

    public int ManhattanDistanceTo(TileCoordinate other)
    {
        return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
    }

    public override string ToString() => $"({X}, {Y})";
}

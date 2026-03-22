using GameCore.Common;

namespace GameCore.Tests;

public class ValueTypeTests
{
    [Fact]
    public void GridCoordinate_GetNeighbors_returns_4_neighbors()
    {
        var coord = new GridCoordinate(5, 5);

        var neighbors = coord.GetNeighbors().ToList();

        Assert.Equal(4, neighbors.Count);
        Assert.Contains(new GridCoordinate(4, 5), neighbors);
        Assert.Contains(new GridCoordinate(6, 5), neighbors);
        Assert.Contains(new GridCoordinate(5, 4), neighbors);
        Assert.Contains(new GridCoordinate(5, 6), neighbors);
    }

    [Fact]
    public void GridCoordinate_DistanceTo_calculates_correctly()
    {
        var a = new GridCoordinate(0, 0);
        var b = new GridCoordinate(3, 4);

        var distance = a.DistanceTo(b);

        Assert.Equal(5f, distance, precision: 3);
    }

    [Fact]
    public void Percentage_clamps_to_0_1()
    {
        var tooHigh = new Percentage(2.0f);
        var tooLow = new Percentage(-0.5f);
        var normal = new Percentage(0.5f);

        Assert.Equal(1f, tooHigh.Value);
        Assert.Equal(0f, tooLow.Value);
        Assert.Equal(0.5f, normal.Value);
    }

    [Fact]
    public void ResourceAmount_creates_correct_type()
    {
        var gold = new ResourceAmount("Gold", 100);

        Assert.Equal("Gold", gold.Type);
        Assert.Equal(100, gold.Value);
    }
}

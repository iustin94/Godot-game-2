using DungeonKeeper.Core.Common;

namespace DungeonKeeper.Core.Tests;

public class ValueTypeTests
{
    [Fact]
    public void TileCoordinate_GetNeighbors_returns_4_neighbors()
    {
        var coord = new TileCoordinate(5, 5);

        var neighbors = coord.GetNeighbors().ToList();

        Assert.Equal(4, neighbors.Count);
        Assert.Contains(new TileCoordinate(4, 5), neighbors);
        Assert.Contains(new TileCoordinate(6, 5), neighbors);
        Assert.Contains(new TileCoordinate(5, 4), neighbors);
        Assert.Contains(new TileCoordinate(5, 6), neighbors);
    }

    [Fact]
    public void TileCoordinate_DistanceTo_calculates_correctly()
    {
        var a = new TileCoordinate(0, 0);
        var b = new TileCoordinate(3, 4);

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
    public void ResourceAmount_Gold_creates_correct_type()
    {
        var gold = ResourceAmount.Gold(100);

        Assert.Equal(ResourceType.Gold, gold.Type);
        Assert.Equal(100, gold.Value);
    }
}

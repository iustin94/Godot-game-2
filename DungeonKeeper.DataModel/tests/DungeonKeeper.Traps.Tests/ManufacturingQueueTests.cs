using DungeonKeeper.Traps.Manufacturing;

namespace DungeonKeeper.Traps.Tests;

public class ManufacturingQueueTests
{
    private static BlueprintDefinition CreateBlueprint(int manufactureTime = 100) => new()
    {
        Id = "bp-spike",
        Name = "Spike Trap Blueprint",
        Category = BlueprintCategory.Trap,
        ManufactureTime = manufactureTime,
        GoldCost = 500
    };

    [Fact]
    public void Empty_queue_returns_null_CurrentJob()
    {
        var queue = new ManufacturingQueue();
        Assert.Null(queue.CurrentJob);
    }

    [Fact]
    public void Enqueue_adds_to_queue()
    {
        var queue = new ManufacturingQueue();
        queue.Enqueue(CreateBlueprint());

        Assert.Equal(1, queue.Count);
        Assert.NotNull(queue.CurrentJob);
    }

    [Fact]
    public void AddProgress_increases_current_job_progress()
    {
        var queue = new ManufacturingQueue();
        queue.Enqueue(CreateBlueprint(100));

        queue.AddProgress(40);

        Assert.Equal(40, queue.CurrentJob!.ProgressPoints);
    }

    [Fact]
    public void TryCompleteCurrentJob_returns_true_when_job_is_complete()
    {
        var queue = new ManufacturingQueue();
        var blueprint = CreateBlueprint(50);
        queue.Enqueue(blueprint);
        queue.AddProgress(50);

        var completed = queue.TryCompleteCurrentJob(out var result);

        Assert.True(completed);
        Assert.Same(blueprint, result);
        Assert.Equal(0, queue.Count);
    }

    [Fact]
    public void TryCompleteCurrentJob_returns_false_when_job_is_not_complete()
    {
        var queue = new ManufacturingQueue();
        queue.Enqueue(CreateBlueprint(100));
        queue.AddProgress(10);

        var completed = queue.TryCompleteCurrentJob(out var result);

        Assert.False(completed);
        Assert.Null(result);
    }

    [Fact]
    public void Completed_job_dequeues_and_next_job_becomes_current()
    {
        var queue = new ManufacturingQueue();
        var bp1 = CreateBlueprint(10);
        var bp2 = new BlueprintDefinition
        {
            Id = "bp-door",
            Name = "Door Blueprint",
            Category = BlueprintCategory.Door,
            ManufactureTime = 200,
            GoldCost = 300
        };

        queue.Enqueue(bp1);
        queue.Enqueue(bp2);
        queue.AddProgress(10);
        queue.TryCompleteCurrentJob(out _);

        Assert.Equal(1, queue.Count);
        Assert.Same(bp2, queue.CurrentJob!.Blueprint);
    }
}

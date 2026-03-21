namespace DungeonKeeper.Traps.Manufacturing;

public sealed class ManufacturingQueue
{
    private readonly Queue<ManufacturingJob> _jobs = new();

    public int Count => _jobs.Count;

    public ManufacturingJob? CurrentJob => _jobs.Count > 0 ? _jobs.Peek() : null;

    public void Enqueue(BlueprintDefinition blueprint)
    {
        _jobs.Enqueue(new ManufacturingJob { Blueprint = blueprint });
    }

    public void AddProgress(int points)
    {
        if (_jobs.Count == 0) return;
        _jobs.Peek().ProgressPoints += points;
    }

    public bool TryCompleteCurrentJob(out BlueprintDefinition? completed)
    {
        if (_jobs.Count > 0 && _jobs.Peek().IsComplete)
        {
            completed = _jobs.Dequeue().Blueprint;
            return true;
        }

        completed = null;
        return false;
    }
}

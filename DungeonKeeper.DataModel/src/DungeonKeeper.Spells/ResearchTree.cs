namespace DungeonKeeper.Spells;

public class ResearchTree
{
    private readonly Dictionary<string, ResearchProgress> _progress = new();
    private readonly Dictionary<string, SpellDefinition> _definitions = new();
    private string? _currentResearchTarget;

    public ResearchTree(IReadOnlyList<SpellDefinition> spellDefinitions)
    {
        foreach (var definition in spellDefinitions)
        {
            _definitions[definition.Id] = definition;
            _progress[definition.Id] = new ResearchProgress(
                definition.Id,
                definition.ResearchPointsRequired);
        }
    }

    public bool IsResearched(string spellId)
    {
        if (_definitions.TryGetValue(spellId, out var def) && def.AvailableByDefault)
            return true;

        return _progress.TryGetValue(spellId, out var progress) && progress.IsComplete;
    }

    public bool CanResearch(string spellId)
    {
        if (!_definitions.TryGetValue(spellId, out var definition))
            return false;

        if (definition.AvailableByDefault)
            return false;

        if (IsResearched(spellId))
            return false;

        foreach (var prerequisite in definition.Prerequisites)
        {
            if (!IsResearched(prerequisite))
                return false;
        }

        return true;
    }

    public void AddResearchPoints(string spellId, int points)
    {
        if (!_progress.TryGetValue(spellId, out var progress))
            throw new KeyNotFoundException($"Spell '{spellId}' not found in research tree.");

        progress.PointsAccumulated += points;
    }

    public string? GetCurrentResearchTarget() => _currentResearchTarget;

    public void SetResearchTarget(string spellId)
    {
        if (!CanResearch(spellId))
            throw new InvalidOperationException($"Spell '{spellId}' cannot be researched.");

        _currentResearchTarget = spellId;
    }

    public IReadOnlyList<string> GetAvailableForResearch()
    {
        return _definitions.Keys
            .Where(CanResearch)
            .ToList()
            .AsReadOnly();
    }
}

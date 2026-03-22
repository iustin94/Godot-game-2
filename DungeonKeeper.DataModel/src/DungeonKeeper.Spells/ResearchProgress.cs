namespace DungeonKeeper.Spells;

public class ResearchProgress
{
    public string SpellId { get; }
    public int PointsAccumulated { get; set; }
    public int PointsRequired { get; }
    public bool IsComplete => PointsAccumulated >= PointsRequired;

    public ResearchProgress(string spellId, int pointsRequired)
    {
        SpellId = spellId;
        PointsRequired = pointsRequired;
    }
}

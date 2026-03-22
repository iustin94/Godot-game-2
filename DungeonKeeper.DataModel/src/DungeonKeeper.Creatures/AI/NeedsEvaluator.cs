using DungeonKeeper.Creatures.Components;

namespace DungeonKeeper.Creatures.AI;

public static class NeedsEvaluator
{
    private const int AngerTickThreshold = 500;

    /// <summary>
    /// Evaluates creature needs and returns the most urgent state transition, or null if no urgent need.
    /// </summary>
    public static CreatureState? EvaluateUrgentNeed(
        NeedsComponent needs,
        MoraleComponent morale,
        bool hatcheryExists)
    {
        // Angry creature leaves if anger persists
        if (morale.State == MoraleState.Angry && morale.AngerTicks > AngerTickThreshold)
            return CreatureState.Leaving;

        // Hunger is critical
        if (needs.Hunger > 0.7f && hatcheryExists)
            return CreatureState.GoingToEat;

        // Very tired and has a lair
        if (needs.Tiredness > 0.8f && needs.HasLair)
            return CreatureState.GoingToLair;

        return null;
    }
}

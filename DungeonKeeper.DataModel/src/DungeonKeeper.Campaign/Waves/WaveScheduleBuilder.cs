using DungeonKeeper.Campaign.MapBlueprint;
using DungeonKeeper.Keeper.HeroInvasion;

namespace DungeonKeeper.Campaign.Waves;

public static class WaveScheduleBuilder
{
    public static void PopulateScheduler(
        InvasionScheduler scheduler,
        IReadOnlyList<WaveDefinition> waves,
        IReadOnlyList<HeroGateDefinition> gates)
    {
        var gateLookup = gates.ToDictionary(g => g.GateId, g => g.Location);

        foreach (var wave in waves)
        {
            if (!gateLookup.TryGetValue(wave.SourceGateId, out var entryPoint))
                continue;

            var invasionWave = new InvasionWave
            {
                WaveNumber = wave.WaveNumber,
                ScheduledTick = wave.ScheduledTick,
                EntryPoint = entryPoint,
                Groups = wave.Groups
            };

            scheduler.ScheduleWave(invasionWave);
        }
    }
}

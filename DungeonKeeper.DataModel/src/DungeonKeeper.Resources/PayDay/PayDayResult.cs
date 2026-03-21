using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Resources.PayDay;

public record PayDayResult(
    int TotalPaid,
    IReadOnlyList<EntityId> PaidCreatures,
    IReadOnlyList<EntityId> UnpaidCreatures
);

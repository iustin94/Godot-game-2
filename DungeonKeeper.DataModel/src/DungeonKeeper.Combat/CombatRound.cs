using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Combat;

public record CombatRound(
    IReadOnlyList<AttackResult> Attacks,
    IReadOnlyList<EntityId> Casualties,
    IReadOnlyList<EntityId> FledCreatures
);

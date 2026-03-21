namespace DungeonKeeper.Spells;

public record SpellCastResult(bool Success, string? FailureReason = null);

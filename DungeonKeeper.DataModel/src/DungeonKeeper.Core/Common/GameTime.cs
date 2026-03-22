namespace DungeonKeeper.Core.Common;

public readonly record struct GameTime(
    int TickNumber,
    float DeltaSeconds,
    float TotalSeconds
);

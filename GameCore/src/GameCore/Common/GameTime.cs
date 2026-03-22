namespace GameCore.Common;

public readonly record struct GameTime(
    int TickNumber,
    float DeltaSeconds,
    float TotalSeconds
);

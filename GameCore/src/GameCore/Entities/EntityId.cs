namespace GameCore.Entities;

public readonly record struct EntityId(Guid Value)
{
    public static EntityId New() => new(Guid.NewGuid());
    public static EntityId Empty => new(Guid.Empty);
    public override string ToString() => Value.ToString()[..8];
}

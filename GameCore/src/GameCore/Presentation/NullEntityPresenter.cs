using GameCore.Common;
using GameCore.Entities;

namespace GameCore.Presentation;

public sealed class NullEntityPresenter : IEntityPresenter
{
    public static readonly NullEntityPresenter Instance = new();

    public void OnSpawned(EntityId id, GridCoordinate position) { }
    public void OnMoved(EntityId id, GridCoordinate from, GridCoordinate to) { }
    public void OnDestroyed(EntityId id) { }
    public void OnStateChanged(EntityId id, string stateName) { }
}

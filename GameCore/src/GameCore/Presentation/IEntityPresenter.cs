using GameCore.Common;
using GameCore.Entities;

namespace GameCore.Presentation;

public interface IEntityPresenter
{
    void OnSpawned(EntityId id, GridCoordinate position);
    void OnMoved(EntityId id, GridCoordinate from, GridCoordinate to);
    void OnDestroyed(EntityId id);
    void OnStateChanged(EntityId id, string stateName);
}

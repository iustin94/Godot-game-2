using GameCore.Common;
using GameCore.Entities;
using GameCore.Presentation;

namespace GameCore.Tests;

public class PresenterTests
{
    [Fact]
    public void NullEntityPresenter_does_not_throw()
    {
        var presenter = NullEntityPresenter.Instance;
        var id = EntityId.New();
        var pos = new GridCoordinate(1, 1);

        var ex = Record.Exception(() =>
        {
            presenter.OnSpawned(id, pos);
            presenter.OnMoved(id, pos, new GridCoordinate(2, 2));
            presenter.OnStateChanged(id, "idle");
            presenter.OnDestroyed(id);
        });

        Assert.Null(ex);
    }

    [Fact]
    public void PresenterComponent_stores_presenter_reference()
    {
        var presenter = NullEntityPresenter.Instance;
        var component = new PresenterComponent(presenter);

        Assert.Same(presenter, component.Presenter);
    }

    [Fact]
    public void Entity_can_hold_PresenterComponent()
    {
        var entity = new Entity();
        var component = new PresenterComponent(NullEntityPresenter.Instance);

        entity.AddComponent(component);

        Assert.True(entity.HasComponent<PresenterComponent>());
        Assert.Same(component, entity.GetComponent<PresenterComponent>());
    }
}

using GameCore.Entities;

namespace GameCore.Presentation;

public class PresenterComponent : IComponent
{
    public IEntityPresenter Presenter { get; set; }

    public PresenterComponent(IEntityPresenter presenter)
    {
        Presenter = presenter;
    }
}

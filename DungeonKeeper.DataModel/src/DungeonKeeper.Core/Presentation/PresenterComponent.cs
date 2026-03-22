using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Core.Presentation;

public class PresenterComponent : IComponent
{
    public IEntityPresenter Presenter { get; set; }

    public PresenterComponent(IEntityPresenter presenter)
    {
        Presenter = presenter;
    }
}

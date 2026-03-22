using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Core.Presentation;

public interface IPresentationFactory
{
    ICreaturePresenter CreateCreaturePresenter(EntityId entityId, string assetId);
    IRoomPresenter CreateRoomPresenter(EntityId roomId, string assetId);
    IMapPresenter CreateMapPresenter();
    ISpellPresenter CreateSpellPresenter();
    ICombatPresenter CreateCombatPresenter();
    ITrapPresenter CreateTrapPresenter(EntityId entityId, string assetId);
    IAudioPresenter CreateAudioPresenter();
}

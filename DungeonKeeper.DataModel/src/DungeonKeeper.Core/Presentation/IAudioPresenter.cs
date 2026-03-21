using DungeonKeeper.Core.Common;

namespace DungeonKeeper.Core.Presentation;

public interface IAudioPresenter
{
    void PlaySound(string soundId, TileCoordinate? position = null);
    void PlayMusic(string trackId);
    void OnMentorMessage(string messageId);
}

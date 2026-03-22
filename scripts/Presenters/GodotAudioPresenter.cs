using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Presentation;
using Godot;

namespace DungeonKeeper.Scripts.Presenters;

public class GodotAudioPresenter : IAudioPresenter
{
    public void PlaySound(string soundId, TileCoordinate? position = null)
    {
        GD.Print($"[Audio] Sound: {soundId}" + (position != null ? $" at ({position.Value.X},{position.Value.Y})" : ""));
    }

    public void PlayMusic(string trackId)
    {
        GD.Print($"[Audio] Music: {trackId}");
    }

    public void OnMentorMessage(string messageId)
    {
        GD.Print($"[Mentor] {messageId}");
    }
}

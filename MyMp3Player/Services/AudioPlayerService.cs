using Plugin.Maui.Audio;

namespace MyMp3Player.Services;

public class AudioPlayerService
{
    public async Task Play(IAudioManager audioManager)
    {
        var file = await FileSystem.OpenAppPackageFileAsync("Audio/run-staccato-string.mp3");
        var player = audioManager.CreatePlayer(file);
        player.Play();
    }
}
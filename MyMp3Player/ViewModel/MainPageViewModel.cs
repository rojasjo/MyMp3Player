using MyMp3Player.Services;
using Plugin.Maui.Audio;

namespace MyMp3Player.ViewModel;

public class MainPageViewModel
{
    public Task Play(IAudioManager audioManager)
    {
        var audioPlayer = new AudioPlayerService();
        return audioPlayer.Play(audioManager);
    }
}
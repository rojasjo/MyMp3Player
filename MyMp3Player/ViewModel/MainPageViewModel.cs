using MyMp3Player.Services;
using Plugin.Maui.Audio;

namespace MyMp3Player.ViewModel;

public class MainPageViewModel
{
    public Task Play(AudioManager audioManager)
    {
        var audioPlayer = new AudioPlayerService();
        return audioPlayer.Play(audioManager);
    }
}
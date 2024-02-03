using Plugin.Maui.Audio;

namespace MyMp3Player.Services;

public class AudioPlayerService : IAudioPlayerService
{
    private readonly IAudioManager _audioManager;

    public AudioPlayerService(IAudioManager audioManager)
    {
        _audioManager = audioManager;
    }
    
    public void Play(Stream audioStream)
    {
        var player = _audioManager.CreatePlayer(audioStream);
        player.Play();
    }
}
using Plugin.Maui.Audio;

namespace MyMp3Player.Services;

public class AudioPlayerService : IAudioPlayerService
{
    private readonly IAudioManager _audioManager;
    
    private IDictionary<string, IAudioPlayer> _players;
    private string _currentSong = string.Empty;
    
    public event Action? SongEnded;

    public AudioPlayerService(IAudioManager audioManager)
    {
        _audioManager = audioManager;
        _players = new Dictionary<string, IAudioPlayer>();
    }

    public void Play(string name, Stream? audioStream = null)
    {
        if (!_players.TryGetValue(name, out var audioPlayer))
        {
            audioPlayer = _audioManager.CreatePlayer(audioStream);
            _players[name] = audioPlayer;
            audioPlayer.PlaybackEnded += PlayerOnPlaybackEnded;
        }
        
        var playing = _players.FirstOrDefault(p => p.Value.IsPlaying).Value;
        playing?.Stop();
        
        audioPlayer.Play();
        _currentSong = name;
    }

    private void PlayerOnPlaybackEnded(object? sender, EventArgs e)
    {
        SongEnded?.Invoke();
    }
}
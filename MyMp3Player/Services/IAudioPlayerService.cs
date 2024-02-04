namespace MyMp3Player.Services;

public interface IAudioPlayerService
{
    void Play(string name, Stream? audioStream = null);

    event Action SongEnded;
}
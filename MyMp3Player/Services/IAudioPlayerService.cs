namespace MyMp3Player.Services;

public interface IAudioPlayerService
{
    void Play(Stream audioStream);
}
namespace MyMp3Player.Services;

public interface IAudioFileStreamProvider
{
    Task<Stream> GetStream(string filename);
}
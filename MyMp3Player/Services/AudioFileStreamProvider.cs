namespace MyMp3Player.Services;

public class AudioFileStreamProvider : IAudioFileStreamProvider
{
    public Task<Stream> GetStream(string filename)
    {
        return FileSystem.OpenAppPackageFileAsync($"Audio/{filename}");
    }
}
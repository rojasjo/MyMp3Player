using MyMp3Player.Exceptions;
using MyMp3Player.Services;

namespace MyMp3Player.ViewModel;

public class MainPageViewModel : IMainPageViewModel
{
    private readonly IAudioFileStreamProvider _audioFileStreamProvider;
    private readonly IAudioPlayerService _audioPlayerService;

    public MainPageViewModel(IAudioFileStreamProvider audioFileStreamProvider, IAudioPlayerService audioPlayerService)
    {
        _audioFileStreamProvider = audioFileStreamProvider;
        _audioPlayerService = audioPlayerService;
    }

    public async Task Play(string audioFile)
    {
        try
        {
            var stream = await _audioFileStreamProvider.GetStream(audioFile);
            _audioPlayerService.Play(stream);
        }
        catch (Exception ex)
        {
            throw new CannotReproduceAudioException($"Failed to reproduce the file {audioFile}", ex);
        }
    }
}
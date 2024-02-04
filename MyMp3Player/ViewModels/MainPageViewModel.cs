using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MyMp3Player.Exceptions;
using MyMp3Player.Models;
using MyMp3Player.Services;

namespace MyMp3Player.ViewModels;

public class MainPageViewModel : IMainPageViewModel
{
    private readonly IAudioFileStreamProvider _audioFileStreamProvider;
    private readonly IAudioPlayerService _audioPlayerService;
    private readonly IDataService _dataService;

    public Song SelectedSong { get; set; }

    public ObservableCollection<Song> Songs { get; }

    public IAsyncRelayCommand SelectionChangedCommand { get; private set; }
    
    public MainPageViewModel(IAudioFileStreamProvider audioFileStreamProvider, IAudioPlayerService audioPlayerService,
        IDataService dataService)
    {
        _audioFileStreamProvider = audioFileStreamProvider;
        _audioPlayerService = audioPlayerService;
        _dataService = dataService;

        Songs = new ObservableCollection<Song>();
        SelectionChangedCommand = new AsyncRelayCommand(async () => await SelectionChanged());
        
    }
    
    public async Task Load()
    {
        Songs.Clear();
        
        foreach (var song in await _dataService.LoadSongs())
        {
            Songs.Add(song);
        }
    }

    private Task SelectionChanged()
    {
        return Play(SelectedSong.Filename);
    }

    private async Task Play(string audioFile)
    {
        try
        {
            var stream = await _audioFileStreamProvider.GetStream(audioFile);
            _audioPlayerService.Play(audioFile, stream);
        }
        catch (Exception ex)
        {
            throw new CannotReproduceAudioException($"Failed to reproduce the file {audioFile}", ex);
        }
    }
}
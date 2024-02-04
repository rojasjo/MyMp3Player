using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMp3Player.Exceptions;
using MyMp3Player.Models;
using MyMp3Player.Services;

namespace MyMp3Player.ViewModels;

public class MainPageViewModel : ObservableRecipient, IMainPageViewModel
{
    private readonly IAudioFileStreamProvider _audioFileStreamProvider;
    private readonly IAudioPlayerService _audioPlayerService;
    private readonly IDataService _dataService;

    private readonly SongList _songList;

    private Song _selectedSong;

    public Song SelectedSong
    {
        get => _selectedSong;
        set
        {
            if (value == _selectedSong)
            {
                return;
            }

            _selectedSong = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Song> Songs { get; }

    public IAsyncRelayCommand SelectionChangedCommand { get; }

    public MainPageViewModel(IAudioFileStreamProvider audioFileStreamProvider, IAudioPlayerService audioPlayerService,
        IDataService dataService)
    {
        _audioFileStreamProvider = audioFileStreamProvider;
        _audioPlayerService = audioPlayerService;
        _dataService = dataService;

        Songs = new ObservableCollection<Song>();
        SelectionChangedCommand = new AsyncRelayCommand(SelectionChanged);

        _audioPlayerService.SongEnded += OnSongEnded;
        _songList = new SongList();
    }

    private void OnSongEnded()
    {
        var next = _songList.GetNext(SelectedSong);
        SelectedSong = next;
    }

    public async Task Load()
    {
        Songs.Clear();
        var songs = await _dataService.LoadSongs();

        _songList.AddRange(songs);

        foreach (var song in songs)
        {
            Songs.Add(song);
        }
    }

    private Task SelectionChanged()
    {
        return Play(SelectedSong);
    }

    private async Task Play(Song song)
    {
        try
        {
            Stream? fileStream = null;
            if (!song.IsLoaded)
            {
                fileStream = await _audioFileStreamProvider.GetStream(song.Filename);
                song.IsLoaded = true;
            }

            _audioPlayerService.Play(song.Filename, fileStream);
        }
        catch (Exception ex)
        {
            throw new CannotReproduceAudioException($"Failed to reproduce the file {song}", ex);
        }
    }
}
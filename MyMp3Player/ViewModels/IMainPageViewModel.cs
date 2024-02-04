using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MyMp3Player.Models;

namespace MyMp3Player.ViewModels;

public interface IMainPageViewModel
{
    public Song SelectedSong { get; set; }
    
    IAsyncRelayCommand SelectionChangedCommand { get; }
    
    public ObservableCollection<Song> Songs { get; }
    
    Task Load();
}
using MyMp3Player.ViewModel;
using Plugin.Maui.Audio;

namespace MyMp3Player;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel _mainPageViewModel;
    private readonly AudioManager _audioManager;

    public MainPage(AudioManager audioManager)
    {
        InitializeComponent();
        
        _audioManager = audioManager;
        _mainPageViewModel = new MainPageViewModel();
    }

    private async void OnPlayClicked(object sender, EventArgs e)
    {
        await _mainPageViewModel.Play(_audioManager);
    }
}
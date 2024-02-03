using Plugin.Maui.Audio;

namespace MyMp3Player;

public partial class MainPage : ContentPage
{
    int count = 0;
    private readonly IAudioManager _audioManager;

    public MainPage(IAudioManager audioManager)
    {
        InitializeComponent();

        _audioManager = audioManager;
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        var file = await FileSystem.OpenAppPackageFileAsync("Audio/01-Cenerentola.mp3");
        var player = _audioManager.CreatePlayer(file);
        player.Play();
    }
}
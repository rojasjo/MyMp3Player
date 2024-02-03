using MyMp3Player.ViewModel;
using Plugin.Maui.Audio;

namespace MyMp3Player;

public partial class MainPage : ContentPage
{
    private readonly IMainPageViewModel _mainPageViewModel;

    public MainPage(IMainPageViewModel mainPageViewModel)
    {
        InitializeComponent();

        _mainPageViewModel = mainPageViewModel;
    }

    private async void OnPlayClicked(object sender, EventArgs e)
    {
        //TODO: get the file name dynamically.
        await _mainPageViewModel.Play("run-staccato-string.mp3");
    }
}
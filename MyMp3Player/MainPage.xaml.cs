using MyMp3Player.ViewModels;
using Plugin.Maui.Audio;

namespace MyMp3Player;

public partial class MainPage : ContentPage
{
    private readonly IMainPageViewModel _mainPageViewModel;

    public MainPage(IMainPageViewModel mainPageViewModel)
    {
        _mainPageViewModel = mainPageViewModel;
        
        InitializeComponent();
        BindingContext = _mainPageViewModel;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _mainPageViewModel.Load();
    }
}
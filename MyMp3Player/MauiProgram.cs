using Microsoft.Extensions.Logging;
using MyMp3Player.Services;
using MyMp3Player.ViewModels;
using Plugin.Maui.Audio;

namespace MyMp3Player;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton(AudioManager.Current);
        builder.Services.AddTransient<IAudioFileStreamProvider, AudioFileStreamProvider>();
        builder.Services.AddTransient<IAudioPlayerService, AudioPlayerService>();
        builder.Services.AddTransient<IMainPageViewModel, MainPageViewModel>();
        builder.Services.AddTransient<IDataService, LocalDataService>();
        builder.Services.AddTransient<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
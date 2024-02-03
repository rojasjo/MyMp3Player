using MyMp3Player.ViewModel;
using Plugin.Maui.Audio;

namespace MyMp3Player.Tests;

[TestFixture]
public class MainPageViewModelTests
{
    private MainPageViewModel _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new MainPageViewModel();
    }
    
    [Test]
    public async Task Play_NoOtherSoundIsPlaying_NoExceptionAreThrown()
    {
        try
        {
            await _sut.Play(AudioManager.Current as AudioManager);
        }
        catch
        {
            Assert.Fail();
        }
    }
}
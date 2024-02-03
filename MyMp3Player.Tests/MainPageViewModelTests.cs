using Moq;
using MyMp3Player.Exceptions;
using MyMp3Player.Services;
using MyMp3Player.ViewModel;

namespace MyMp3Player.Tests;

[TestFixture]
public class MainPageViewModelTests
{
    private MainPageViewModel? _sut;
    
    private Mock<IAudioFileStreamProvider>? _audioFileStreamProviderMock;
    private Mock<IAudioPlayerService>? _audioPlayerServiceMock;
    
    [SetUp]
    public void Setup()
    {
        _audioFileStreamProviderMock = new Mock<IAudioFileStreamProvider>();
        _audioPlayerServiceMock = new Mock<IAudioPlayerService>();
        
        _sut = new MainPageViewModel(_audioFileStreamProviderMock.Object, _audioPlayerServiceMock.Object);
    }
    
    [Test]
    public async Task Play_NoOtherSoundIsPlaying_NoExceptionAreThrown()
    {
        Assert.DoesNotThrowAsync(async () => await _sut.Play("test"));
    }
    
    [Test]
    public async Task Play_FileIsNotFound_ThrowsCannotReproduceAudioException()
    {
        _audioFileStreamProviderMock.Setup(p => p.GetStream(It.IsAny<string>() ))
            .ThrowsAsync(new FileNotFoundException());
        
        Assert.ThrowsAsync<CannotReproduceAudioException>(async () => await _sut.Play("test"));
    }
    
    [Test]
    public async Task Play_AudioManagerFails_ThrowsCannotReproduceAudioException()
    {
        _audioPlayerServiceMock.Setup(p => p.Play(It.IsAny<Stream>() ))
            .Throws<InvalidDataException>();
        
        Assert.ThrowsAsync<CannotReproduceAudioException>(async () => await _sut.Play("test"));
    }
}
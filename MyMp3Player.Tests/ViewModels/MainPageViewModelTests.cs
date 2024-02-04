using Moq;
using MyMp3Player.Exceptions;
using MyMp3Player.Models;
using MyMp3Player.Services;
using MyMp3Player.ViewModels;

namespace MyMp3Player.Tests.ViewModels;

[TestFixture]
public class MainPageViewModelTests
{
    private MainPageViewModel? _sut;

    private Mock<IAudioFileStreamProvider>? _audioFileStreamProviderMock;
    private Mock<IAudioPlayerService>? _audioPlayerServiceMock;
    private Mock<IDataService>? _dataServiceMock;

    [SetUp]
    public void Setup()
    {
        _audioFileStreamProviderMock = new Mock<IAudioFileStreamProvider>();
        _audioPlayerServiceMock = new Mock<IAudioPlayerService>();
        _dataServiceMock = new Mock<IDataService>();

        _sut = new MainPageViewModel(_audioFileStreamProviderMock.Object, _audioPlayerServiceMock.Object,
            _dataServiceMock.Object);

        _sut.SelectedSong = new Song { Title = "test", Filename = "test" };
    }

    [Test]
    public void Play_NoOtherSoundIsPlaying_NoExceptionAreThrown()
    {
        Assert.DoesNotThrow(() => _sut.SelectionChangedCommand.Execute(null));
    }

    [Test]
    public void Play_FileIsNotFound_ThrowsCannotReproduceAudioException()
    {
        _audioFileStreamProviderMock.Setup(p => p.GetStream(It.IsAny<string>()))
            .ThrowsAsync(new FileNotFoundException());

        Assert.ThrowsAsync<CannotReproduceAudioException>(async () => await _sut.SelectionChangedCommand.ExecuteAsync(null));
    }

    [Test]
    public void Play_AudioManagerFails_ThrowsCannotReproduceAudioException()
    {
        _audioPlayerServiceMock.Setup(p => p.Play(It.IsAny<string>(), It.IsAny<Stream>()))
            .Throws<InvalidDataException>();

        Assert.ThrowsAsync<CannotReproduceAudioException>(async () => await _sut.SelectionChangedCommand.ExecuteAsync(null));
    }

    [Test]
    public async Task Play_Always_GetStreamIsCalledOnce()
    {
        await _sut.SelectionChangedCommand.ExecuteAsync(null);

        Assert.Multiple(() =>
        {
            _audioFileStreamProviderMock.Verify(p => p.GetStream(It.Is<string>(p => p == "test")), Times.Once);
            _audioFileStreamProviderMock.Verify(p => p.GetStream(It.IsAny<string>()), Times.Once);
        });
    }
    
    [Test]
    public async Task Play_SameSongPlayedTwice_StreamIsLoadedOnlyOnce()
    {
        await _sut.SelectionChangedCommand.ExecuteAsync(null);
        await _sut.SelectionChangedCommand.ExecuteAsync(null);
        
        _audioFileStreamProviderMock.Verify(p => p.GetStream(It.IsAny<string>()), Times.Once);
    }
    
    [Test]
    public async Task Play_TowDifferentSongsPlayed_StreamAreLoaded()
    {
        await _sut.SelectionChangedCommand.ExecuteAsync(null);
        
        _sut.SelectedSong = new Song { Title = "test1", Filename = "test1" };
        await _sut.SelectionChangedCommand.ExecuteAsync(null);

        _audioFileStreamProviderMock.Verify(p => p.GetStream(It.IsAny<string>()), Times.Exactly(2));
    }
    
    [Test]
    public async Task Play_TowDifferentSongsPlayedAlreadyLoaded_StreamAreNotLoaded()
    {
        _sut.SelectedSong = new Song { Title = "test1", Filename = "test1", IsLoaded = true};
        await _sut.SelectionChangedCommand.ExecuteAsync(null);
        
        _sut.SelectedSong = new Song { Title = "test2", Filename = "test2", IsLoaded = true};
        await _sut.SelectionChangedCommand.ExecuteAsync(null);

        _audioFileStreamProviderMock.Verify(p => p.GetStream(It.IsAny<string>()), Times.Never);
    }
}
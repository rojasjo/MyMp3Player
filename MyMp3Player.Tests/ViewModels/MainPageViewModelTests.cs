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
    public void Play_Always_GetStreamIsCalledOnce()
    {
        _sut.SelectionChangedCommand.Execute(null);

        Assert.Multiple(() =>
        {
            _audioFileStreamProviderMock.Verify(p => p.GetStream(It.Is<string>(p => p == "test")), Times.Once);
            _audioFileStreamProviderMock.Verify(p => p.GetStream(It.IsAny<string>()), Times.Once);
        });
    }
}
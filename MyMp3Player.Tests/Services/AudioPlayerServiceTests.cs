using Moq;
using MyMp3Player.Services;
using Plugin.Maui.Audio;

namespace MyMp3Player.Tests.Services;

[TestFixture]
public class AudioPlayerServiceTests
{
    private AudioPlayerService _sut;
    private Mock<IAudioManager> _audioManagerMock;

    [SetUp]
    public void Setup()
    {
        _audioManagerMock = new Mock<IAudioManager>();
        _sut = new AudioPlayerService(_audioManagerMock.Object);
    }

    [Test]
    public void Play_ValidStream_RegisteredToPlaybackEndedEvent()
    {
        var playerMock = new Mock<IAudioPlayer>();
        _audioManagerMock.Setup(m => m.CreatePlayer(It.IsAny<Stream>()))
            .Returns(playerMock.Object);
        
        _sut.Play("test", new MemoryStream());
        
        playerMock.VerifyAdd(p => p.PlaybackEnded += It.IsAny<EventHandler>(), Times.Once);
    }

    [Test]
    public void SongEnded_PlaybackEndedIsRaised_SongEndedIsInvoked()
    {
        //Arrange
        var playerMock = new Mock<IAudioPlayer>();
        _audioManagerMock.Setup(m => m.CreatePlayer(It.IsAny<Stream>()))
            .Returns(playerMock.Object);
        
        var songEndedInvoked = false;
        _sut.SongEnded += () => songEndedInvoked = true;
        _sut.Play("test", new MemoryStream());
     
        //Act
        playerMock.Raise(p => p.PlaybackEnded += null, null, null);
        
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(songEndedInvoked, Is.True);
            playerMock.VerifyRemove(p => p.PlaybackEnded -= It.IsAny<EventHandler>(), Times.Never);
        });
    }
}
using MyMp3Player.Models;

namespace MyMp3Player.Tests.Models;

using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class SongListTests
{
    private SongList _sut;
    private List<Song> _songs;

    [SetUp]
    public void Setup()
    {
        _sut = new SongList();
        _songs = new List<Song>
        {
            new() { Title = "Song1" },
            new() { Title = "Song2" },
            new() { Title = "Song3" }
        };
    }

    [Test]
    public void GetPrevious_ListIsEmpty_ReturnsNull()
    {
        var previous = _sut.GetPrevious(_songs[0]);

        Assert.That(previous, Is.Null);
    }

    [Test]
    public void GetNext_ListIsEmpty_ReturnsNull()
    {
        var next = _sut.GetNext(_songs[0]);

        Assert.That(next, Is.Null);
    }

    [Test]
    [TestCase(0,2)]
    [TestCase(1,0)]
    [TestCase(2,1)]
    public void GetPrevious_HasThreeItems_ReturnsExpectedSong(int currentIndex, int expectedIndex)
    {
        // Arrange
        _sut.AddRange(_songs);

        // Act
        var previousSong = _sut.GetPrevious(_songs[currentIndex]);

        // Assert
        Assert.That(previousSong, Is.EqualTo(_songs[expectedIndex]));
    }
    
    [Test]
    [TestCase(0,1)]
    [TestCase(1,2)]
    [TestCase(2,0)]
    public void GetNext_HasThreeItems_ReturnsExpectedSong(int currentIndex, int expectedIndex)
    {
        // Arrange
        _sut.AddRange(_songs);

        // Act
        var previousSong = _sut.GetNext(_songs[currentIndex]);

        // Assert
        Assert.That(previousSong, Is.EqualTo(_songs[expectedIndex]));
    }
}
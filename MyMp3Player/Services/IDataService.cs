using MyMp3Player.Models;

namespace MyMp3Player.Services;

public interface IDataService
{
    Task<IList<Song>> LoadSongs();
}
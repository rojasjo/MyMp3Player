using MyMp3Player.Models;

namespace MyMp3Player.Services;

public class LocalDataService : IDataService
{
    public async Task<IList<Song>> LoadSongs()
    {
        return await Task.FromResult(new List<Song>()
        {
            new()
            {
                Title = "Song 1",
                Filename = "run-staccato-string.mp3"
            },
            new()
            {
                Title = "Song 2",
                Filename = "ragtime-logo.mp3"
            },
            new()
            {
                Title = "Song 3",
                Filename = "medieval.mp3"
            }
        });
    }
}
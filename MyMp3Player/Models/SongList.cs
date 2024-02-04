namespace MyMp3Player.Models;

public class SongList
{
    private readonly List<Song> _songOrder = new();

    public void AddRange(IEnumerable<Song> songs)
    {
        _songOrder.AddRange(songs);
    }

    public Song? GetPrevious(Song song)
    {
        var index = _songOrder.IndexOf(song);

        if (index < 0)
        {
            return null;
        }
        
        return index > 0 ? _songOrder[index - 1] : _songOrder[^1];
    }

    public Song? GetNext(Song song)
    {
        var index = _songOrder.IndexOf(song);

        if (index < 0)
        {
            return null;
        }
        
        return index < _songOrder.Count - 1 ? _songOrder[index + 1] : _songOrder[0];
    }
}
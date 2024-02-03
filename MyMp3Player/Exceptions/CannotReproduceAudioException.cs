namespace MyMp3Player.Exceptions;

public class CannotReproduceAudioException : Exception
{
    public CannotReproduceAudioException(string message, Exception innerException) : base(message, innerException)
    {
        
    }
}
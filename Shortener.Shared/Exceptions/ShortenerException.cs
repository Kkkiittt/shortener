namespace Shortener.Shared.Exceptions;

public class ShortenerException : Exception
{
	public ShortenerException(string message) : base(message)
	{
	}
}

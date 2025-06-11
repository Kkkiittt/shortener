namespace Shortener.Shared.Exceptions;

public class ShortenerUsedException : ShortenerArgumentException
{
	public ShortenerUsedException(string message, string fieldName) : base(message, fieldName)
	{
	}
}

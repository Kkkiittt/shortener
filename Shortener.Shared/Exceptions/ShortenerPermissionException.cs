namespace Shortener.Shared.Exceptions;

public class ShortenerPermissionException : ShortenerException
{
	public string Restriction { get; set; }
	public ShortenerPermissionException(string message, string restriction) : base(message)
	{
		Restriction = restriction;
	}
}

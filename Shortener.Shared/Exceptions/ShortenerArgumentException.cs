namespace Shortener.Shared.Exceptions;

public class ShortenerArgumentException : ShortenerException
{
	public string FieldName { get; set; }

	public ShortenerArgumentException(string message, string fieldName) : base(message)
	{
		FieldName = fieldName;
	}
}

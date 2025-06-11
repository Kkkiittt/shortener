namespace Shortener.Shared.Exceptions;

public class ShortenerNotFoundException : ShortenerException
{
	public string EntityName { get; set; }
	public string EntityId { get; set; }

	public ShortenerNotFoundException(string message, string entityName, string entityId) : base(message)
	{
		EntityName = entityName;
		EntityId = entityId;
	}
}

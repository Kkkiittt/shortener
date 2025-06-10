using Shortener.Shared.Enums;

namespace Shortener.Shared.Interfaces;

public interface IUserIdentifier
{
	public long Id { get; }
	public long SubscriptionId { get; }
	public Roles Role { get; }
	public bool IsAdmin { get; }
}

using Authenticator.Domain.Enums;

namespace Authenticator.Application.Interfaces.Services;

public interface IUserIdentifier
{
	public long Id { get; }
	public string Email { get; }
	public long SubscriptionId { get; }
	public Roles Role { get; }
	public bool Admin { get; }
}

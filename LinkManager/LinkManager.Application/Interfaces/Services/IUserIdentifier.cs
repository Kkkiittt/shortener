using Authenticator.Domain.Enums;

namespace LinkManager.Application.Interfaces.Services;

public interface IUserIdentifier
{
	public long Id { get; }

	public long PlanId { get; }

	public Roles Role { get; }

	public bool IsAdmin{ get; }
}

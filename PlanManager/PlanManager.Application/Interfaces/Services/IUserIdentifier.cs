using Authenticator.Domain.Enums;

namespace PlanManager.Application.Interfaces.Services;

public interface IUserIdentifier
{
	public long Id { get; }

	public bool IsAdmin { get; }

	public Roles Role { get; }
}

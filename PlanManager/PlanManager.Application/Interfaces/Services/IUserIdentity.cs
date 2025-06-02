using Authenticator.Domain.Enums;

namespace PlanManager.Application.Interfaces.Services;

public interface IUserIdentity
{
	public long Id { get; }

	public bool Admin { get; }

	public Roles Role { get; }
}

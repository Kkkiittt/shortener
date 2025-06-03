using System.Security.Claims;

using Authenticator.Domain.Enums;

using PlanManager.Application.Interfaces.Services;

namespace PlanManager.Api.Services;

public class UserIdentifier : IUserIdentifier
{
	private readonly IHttpContextAccessor _accessor;

	public UserIdentifier(IHttpContextAccessor accessor)
	{
		_accessor = accessor;
	}

	public long Id
	{
		get
		{
			return long.Parse(_accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("Invalid token"));
		}
	}

	public bool IsAdmin
	{
		get
		{
			return Role == Roles.Admin || Role == Roles.Owner;
		}
	}

	public Roles Role
	{
		get
		{
			return Enum.Parse<Roles>(_accessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value ?? throw new InvalidOperationException("Invalid token"));
		}
	}
}

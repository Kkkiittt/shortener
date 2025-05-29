
using System.Security.Claims;

using Authenticator.Domain.Enums;

using LinkManager.Application.Interfaces.Services;

namespace LinkManager.Api.Services;

public class UserIdentifier : IUserIdentifier
{
	private readonly IHttpContextAccessor _context;

	public UserIdentifier(IHttpContextAccessor context)
	{
		_context = context;
	}

	public long Id
	{
		get
		{
			return long.Parse(_context.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("User not found"));
		}
	}

	public long PlanId
	{
		get
		{
			return long.Parse(_context.HttpContext?.User.FindFirst("Subscription")?.Value ?? throw new Exception("User not found"));
		}
	}

	public Roles Role
	{
		get
		{
			return Enum.Parse<Roles>(_context.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value ?? throw new Exception("User not found"));
		}
	}

	public bool IsAdmin
	{
		get
		{
			return Role == Roles.Admin || Role == Roles.Owner;
		}
	}
}

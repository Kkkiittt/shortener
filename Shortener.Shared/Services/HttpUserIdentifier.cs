using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using Shortener.Shared.Enums;
using Shortener.Shared.Interfaces;




namespace Shortener.Shared.Services;

public class HttpUserIdentifier : IUserIdentifier
{
	private readonly IHttpContextAccessor _accessor;

	public HttpUserIdentifier(IHttpContextAccessor accessor)
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

	public string Email
	{
		get
		{
			return _accessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value ?? throw new InvalidOperationException("Invalid token");
		}
	}

	public long SubscriptionId
	{
		get
		{
			return long.Parse(_accessor.HttpContext?.User.FindFirst("Subscription")?.Value ?? throw new InvalidOperationException("Invalid token"));
		}
	}

	public Roles Role
	{
		get
		{
			return (Roles)Enum.Parse(typeof(Roles), _accessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value ?? throw new InvalidOperationException("Invalid token" + (_accessor.HttpContext?.User.Claims.Count())));
		}
	}

	public bool IsAdmin
	{
		get
		{
			return Role == Roles.Admin || Role == Roles.Owner;
		}
	}

	public DateTime IssueTime
	{
		get
		{
			return DateTimeOffset.FromUnixTimeSeconds(int.Parse(_accessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Iat)?.Value ?? throw new InvalidOperationException("Invalid token"))).UtcDateTime;
		}
	}

	public DateTime ExpireTime
	{
		get
		{
			return DateTime.Parse(_accessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Exp)?.Value ?? throw new InvalidOperationException("Invalid token"));
		}
	}
}

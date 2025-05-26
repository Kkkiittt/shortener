using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Authenticator.Application.Interfaces.Services;
using Authenticator.Domain.Enums;

using Microsoft.AspNetCore.Http;

namespace Authenticator.Application.Services;

public class UserIdentifier : IUserIdentifier
{
	IHttpContextAccessor _accessor;

	public UserIdentifier(IHttpContextAccessor accessor)
	{
		_accessor = accessor;
	}

	public long Id
	{
		get
		{
			return long.Parse(_accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("Invalid token"));
		}
	}

	public string Email
	{
		get
		{
			return _accessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value ?? throw new InvalidOperationException("Invalid token");
		}
	}

	public long SubscriptionId
	{
		get
		{
			return long.Parse(_accessor.HttpContext.User.FindFirst("Subscription")?.Value ?? throw new InvalidOperationException("Invalid token"));
		}
	}

	public Roles Role
	{
		get
		{
			return (Roles)Enum.Parse(typeof(Roles), _accessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value ?? throw new InvalidOperationException("Invalid token"));
		}
	}

	public bool Admin
	{
		get
		{
			return Role == Roles.Admin || Role == Roles.Owner;
		}
	}
}

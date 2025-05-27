using Authenticator.Application.Dtos;
using Authenticator.Application.Interfaces.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authenticator.Api.Controllers;


[ApiController]
[Route("auth/")]
public class AuthController : ControllerBase
{
	private readonly IUserManager _userManager;

	public AuthController(IUserManager userManager)
	{
		_userManager = userManager;
	}

	[HttpPatch("promote/{id}")]
	[Authorize(Roles ="Admin, Owner")]
	public async Task<IActionResult> PromoteAsync(long id)
	{
		try
		{
			var result = await _userManager.PromoteAsync(id);
			if(result)
				return NoContent();
			else
			{
				return BadRequest();
			}
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message+"\n"+ex.StackTrace);
		}
	}

	[HttpPost("login/")]
	[AllowAnonymous]
	public async Task<IActionResult> LoginAsync(UserLoginDto dto)
	{
		try
		{
			var token = await _userManager.LoginAsync(dto);
			return Ok(token);
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}

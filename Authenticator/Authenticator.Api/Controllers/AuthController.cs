using Authenticator.Application.Dtos;
using Authenticator.Application.Interfaces.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authenticator.Api.Controllers;


[ApiController]
[Route("users/")]
public class AuthController : ControllerBase
{
	private readonly IUserManager _userManager;

	public AuthController(IUserManager userManager)
	{
		_userManager = userManager;
	}

	[HttpPatch("promote/{id}")]
	[Authorize(Roles = "Admin, Owner")]
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
			return BadRequest(ex.Message + "\n" + ex.StackTrace);
		}
	}

	[HttpPatch("demote/{id}")]
	[Authorize(Roles = "Admin, Owner")]
	public async Task<IActionResult> DemoteAsync(long id)
	{
		try
		{
			var result = await _userManager.DemoteAsync(id);
			if(result)
				return NoContent();
			else
			{
				return BadRequest();
			}
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message + "\n" + ex.StackTrace);
		}
	}

	[HttpPatch("subscriptions/{subId}")]
	[Authorize]
	public async Task<IActionResult> SubscribeAsync(long subId)
	{
		try
		{
			var result = await _userManager.SubscribeAsync(subId);
			if(result)
				return NoContent();
			else
				return BadRequest();
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
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

	[HttpPost("register/")]
	[AllowAnonymous]
	public async Task<IActionResult> RegisterAsync(UserCreateDto dto)
	{
		try
		{
			var id = await _userManager.CreateAsync(dto);
			return Ok(id);
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPut("update/")]
	[Authorize]
	public async Task<IActionResult> UpdateAsync(UserUpdateDto dto)
	{
		try
		{
			var result = await _userManager.UpdateAsync(dto);
			if(result)
				return NoContent();
			else
				return BadRequest();
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("update/")]
	[Authorize]
	public async Task<IActionResult> GetTemplateAsync()
	{
		try
		{
			var template = await _userManager.GetTemplateAsync();
			if(template == null)
			{
				return BadRequest();
			}
			return Ok(template);
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpDelete("{id}/")]
	[Authorize]
	public async Task<IActionResult> DeleteAsync(long id)
	{
		try
		{
			var result = await _userManager.DeleteAsync(id);
			if(result)
				return NoContent();
			else
				return BadRequest();
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("{id}/")]
	[Authorize]
	public async Task<IActionResult> GetUserAsync(long id)
	{
		try
		{
			var user = await _userManager.GetUserAsync(id);
			if(user == null)
			{
				return BadRequest();
			}
			return Ok(user);
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
	[HttpGet("")]
	[Authorize(Roles = "Admin, Owner")]
	public async Task<IActionResult> GetUsersAsync(int page, int pageSize)
	{
		try
		{
			var users = await _userManager.GetUsersAsync(page, pageSize);
			if(users == null)
			{
				return BadRequest();
			}
			return Ok(users);
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}

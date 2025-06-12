using Authenticator.Application.Dtos;
using Authenticator.Application.Interfaces.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authenticator.Api.Controllers;

[ApiController]
[Route("users")]
public class AuthController : ControllerBase
{
	private readonly IUserManager _userManager;

	public AuthController(IUserManager userManager)
	{
		_userManager = userManager;
	}

	[HttpPatch("promote/{id}")]
	[Authorize(Roles = "Owner")]
	public async Task<IActionResult> PromoteAsync(long id)
	{
		var result = await _userManager.PromoteAsync(id);
		if(result)
			return NoContent();
		else
			return BadRequest();
	}

	[HttpPatch("demote/{id}")]
	[Authorize(Roles = "Owner")]
	public async Task<IActionResult> DemoteAsync(long id)
	{
		var result = await _userManager.DemoteAsync(id);
		if(result)
			return NoContent();
		else
			return BadRequest();
	}

	[HttpPatch("subscriptions/{subId}")]
	[Authorize]
	public async Task<IActionResult> SubscribeAsync(long subId)
	{
		var result = await _userManager.SubscribeAsync(subId);
		if(result)
			return NoContent();
		else
			return BadRequest();
	}

	[HttpPost("login")]
	[AllowAnonymous]
	public async Task<IActionResult> LoginAsync(UserLoginDto dto)
	{
		var token = await _userManager.LoginAsync(dto);
		return Ok(token);
	}

	[HttpPost("register")]
	[AllowAnonymous]
	public async Task<IActionResult> RegisterAsync(UserCreateDto dto)
	{
		var res = await _userManager.CreateAsync(dto);
		if(res)
			return Created();
		else
			return BadRequest();
	}

	[HttpPut("update")]
	[Authorize]
	public async Task<IActionResult> UpdateAsync(UserUpdateDto dto)
	{
		var result = await _userManager.UpdateAsync(dto);
		if(result)
			return NoContent();
		else
			return BadRequest();
	}

	[HttpGet("update")]
	[Authorize]
	public async Task<IActionResult> GetTemplateAsync()
	{
		var template = await _userManager.GetTemplateAsync();
		if(template == null)
			return BadRequest();
		else
			return Ok(template);
	}

	[HttpDelete("{id}")]
	[Authorize]
	public async Task<IActionResult> DeleteAsync(long id)
	{
		var result = await _userManager.DeleteAsync(id);
		if(result)
			return NoContent();
		else
			return BadRequest();
	}

	[HttpGet("{id}")]
	[Authorize]
	public async Task<IActionResult> GetUserAsync(long id)
	{
		var user = await _userManager.GetUserAsync(id);
		if(user == null)
			return BadRequest();
		else
			return Ok(user);
	}

	[HttpGet("")]
	[Authorize(Roles = "Admin, Owner")]
	public async Task<IActionResult> GetUsersAsync(int page, int pageSize)
	{
		var users = await _userManager.GetUsersAsync(page, pageSize);
		if(users == null)
			return BadRequest();
		else
			return Ok(users);
	}
}

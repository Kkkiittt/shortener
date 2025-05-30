using LinkManager.Application.Dtos;
using LinkManager.Application.Interfaces.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkManager.Api.Controllers;
[ApiController]
[Route("links")]
public class LinkController : ControllerBase
{
	private readonly ILinkService _manager;

	public LinkController(ILinkService manager)
	{
		_manager = manager;
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> CreateLinkAsync(LinkCreateDto dto)
	{
		try
		{
			var res = await _manager.CreateLinkAsync(dto);
			if(res == null)
			{
				return BadRequest();
			}
			return Ok(res);
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("update")]
	public async Task<IActionResult> GetTemplateAsync(string shortLink)
	{
		try
		{
			var res = await _manager.GetTemplateAsync(shortLink);
			if(res == null)
				return BadRequest();
			return Ok(res);
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPut("update")]
	[Authorize]
	public async Task<IActionResult> UpdateLinkAsync(LinkUpdateDto dto)
	{
		try
		{
			var res = await _manager.UpdateLinkAsync(dto);
			if(res)
				return NoContent();
			return BadRequest();
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpDelete("{shortLink}")]
	[Authorize]
	public async Task<IActionResult> DeleteLinkAsync(string shortLink)
	{
		try
		{
			var res = await _manager.DeleteLinkAsync(shortLink);
			if(res)
				return NoContent();
			return BadRequest();
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("{shortLink}")]
	[AllowAnonymous]
	public async Task<IActionResult> GetLinkAsync(string shortLink, string? password = null)
	{
		try
		{
			var res = await _manager.GetLinkAsync(shortLink, password);
			if(res == null)
			{
				return BadRequest();
			}
			return Redirect(res);
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message+ex.StackTrace+" "+shortLink);
		}
	}

	[HttpGet("full-info")]
	[Authorize(Roles = "Admin, Owner")]
	public async Task<IActionResult> GetLinksFullInfoAsync(int page, int pageSize)
	{
		try
		{
			var res = await _manager.GetLinksFullInfoAsync(page, pageSize);
			if(res == null)
			{
				return BadRequest();
			}
			return Ok(res);
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("{id}/full-info")]
	[Authorize(Roles = "Admin, Owner")]
	public async Task<IActionResult> GetLinkFullInfoAsync(long id)
	{
		try
		{
			var res = await _manager.GetLinkFullInfoAsync(id);
			if(res == null)
				return BadRequest();
			return Ok(res);
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("{shortLink}/info")]
	[Authorize]
	public async Task<IActionResult> GetLinkInfoAsync(string shortLink)
	{
		try
		{
			var res = await _manager.GetLinkInfoAsync(shortLink);
			if(res == null)
				return BadRequest();
			return Ok(res);
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("user/{userId}/info")]
	[Authorize]
	public async Task<IActionResult> GetLinksInfoByUserAsync(long userId)
	{
		try
		{
			var res = await _manager.GetLinksInfoByUserAsync(userId);
			if(res == null)
				return BadRequest();
			return Ok(res);
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("user/{userId}/full-info")]
	[Authorize(Roles = "Admin, Owner")]
	public async Task<IActionResult> GetLinksFullInfoByUserAsync(long userId)
	{
		try
		{
			var res = await _manager.GetLinksFullInfoByUserAsync(userId);
			if(res == null)
				return BadRequest();
			return Ok(res);
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}

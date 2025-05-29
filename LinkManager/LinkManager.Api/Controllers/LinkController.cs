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
			return Ok(res);
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
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
}

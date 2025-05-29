using LinkManager.Application.Dtos;
using LinkManager.Application.Interfaces.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkManager.Api.Controllers;
[ApiController]
[Route("links")]
public class LinkManagerController : ControllerBase
{
	private readonly ILinkManager _manager;

	public LinkManagerController(ILinkManager manager)
	{
		_manager = manager;
	}

	[HttpPost]
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
}

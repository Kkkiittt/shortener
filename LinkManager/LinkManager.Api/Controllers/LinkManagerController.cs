using LinkManager.Application.Dtos;
using LinkManager.Application.Interfaces.Services;

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

	public async Task<IActionResult> GetLinkAsync(string shortLink)
	{
		try
		{
			var res = await _manager.GetLinkAsync(shortLink);
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

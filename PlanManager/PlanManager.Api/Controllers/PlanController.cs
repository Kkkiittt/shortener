using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlanManager.Application.Dtos;
using PlanManager.Application.Interfaces.Services;

namespace PlanManager.Api.Controllers;

[ApiController]
[Route("plans")]
public class PlanController : ControllerBase
{
	private readonly IPlanService _planService;

	public PlanController(IPlanService planService)
	{
		_planService = planService;
	}

	[HttpPost("check")]
	[AllowAnonymous]
	public async Task<IActionResult> CheckPlan(PlanCheckDto dto)
	{
		try
		{
			var res = await _planService.CheckPlanActionAsync(dto);
			if(res)
				return Ok();
			return BadRequest();
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPost]
	[Authorize(Roles = "Admin, Owner")]
	public async Task<IActionResult> CreatePlan(PlanCreateDto dto)
	{
		try
		{
			var res = await _planService.CreatePlanAsync(dto);
			if(res)
				return NoContent();
			return BadRequest();
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPut]
	[Authorize(Roles = "Admin, Owner")]
	public async Task<IActionResult> UpdatePlan(PlanUpdateDto dto)
	{
		try
		{
			var res = await _planService.UpdatePlanAsync(dto);
			if(res)
				return NoContent();
			return BadRequest();
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpDelete]
	[Authorize(Roles = "Admin, Owner")]
	public async Task<IActionResult> DeletePlan(int id)
	{
		try
		{
			var res = await _planService.DeletePlanAsync(id);
			if(res)
				return NoContent();
			return BadRequest();
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("{id}")]
	[Authorize(Roles = "Admin, Owner")]
	public async Task<IActionResult> GetPlan(long id)
	{
		try
		{
			var res = await _planService.GetPlanAsync(id);
			return Ok(res);
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet]
	[AllowAnonymous]
	public async Task<IActionResult> GetPlansAsync()
	{
		try
		{
			var res = await _planService.GetPlansShortAsync();
			return Ok(res);
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("full")]
	[Authorize(Roles = "Admin, Owner")]
	public async Task<IActionResult> GetPlansFullAsync()
	{
		try
		{
			var res = await _planService.GetPlansAsync();
			return Ok(res);
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}

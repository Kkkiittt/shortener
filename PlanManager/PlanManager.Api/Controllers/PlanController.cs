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

	//[HttpPost("check")]
	//[AllowAnonymous]
	//public async Task<IActionResult> CheckPlan(PlanCheckDto dto)
	//{
	//	var res = await _planService.CheckPlanActionAsync(dto);
	//	if(res)
	//		return Ok();
	//	return BadRequest();
	//}

	[HttpPost]
	[Authorize(Roles = "Admin, Owner")]
	public async Task<IActionResult> CreatePlan(PlanCreateDto dto)
	{
		var res = await _planService.CreatePlanAsync(dto);
		if(res)
			return NoContent();
		return BadRequest();
	}

	[HttpPut]
	[Authorize(Roles = "Admin, Owner")]
	public async Task<IActionResult> UpdatePlan(PlanUpdateDto dto)
	{
		var res = await _planService.UpdatePlanAsync(dto);
		if(res)
			return NoContent();
		return BadRequest();
	}

	[HttpDelete]
	[Authorize(Roles = "Admin, Owner")]
	public async Task<IActionResult> DeletePlan(int id)
	{
		var res = await _planService.DeletePlanAsync(id);
		if(res)
			return NoContent();
		return BadRequest();
	}

	[HttpGet("{id}")]
	[Authorize(Roles = "Admin, Owner")]
	public async Task<IActionResult> GetPlan(long id)
	{
		var res = await _planService.GetPlanAsync(id);
		return Ok(res);
	}

	[HttpGet]
	[AllowAnonymous]
	public async Task<IActionResult> GetPlansAsync()
	{
		var res = await _planService.GetPlansShortAsync();
		return Ok(res);
	}

	[HttpGet("full")]
	[Authorize(Roles = "Admin, Owner")]
	public async Task<IActionResult> GetPlansFullAsync()
	{
		var res = await _planService.GetPlansAsync();
		return Ok(res);
	}
}

using PlanManager.Application.Dtos;
using PlanManager.Application.Interfaces.Module;
using PlanManager.Application.Interfaces.Services;
using PlanManager.Domain.Entities;

namespace PlanManager.Application.Services;

public class PlanManagerModule : IPlanManagerModule
{
	private readonly IPlanService _planService;

	public PlanManagerModule(IPlanService planService)
	{
		_planService = planService;
	}

	public async Task<bool> CheckPlanAsync(PlanCheckDto dto)
	{
		return await _planService.CheckPlanActionAsync(dto);
	}
}

using PlanManager.Application.Dtos;
using PlanManager.Application.Interfaces.Module;
using PlanManager.Application.Interfaces.Services;

namespace PlanManager.Application.Module;

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

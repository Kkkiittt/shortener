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

	public async Task<bool> CheckPlan(PlanCheckDto dto)
	{
		Plan? plan = await _planService.GetPlanAsync(dto.Id);

		if(plan == null)
			throw new Exception("Plan not found");

		if(!plan.Actions.Contains(dto.Action))
			throw new Exception("Action unavailable");

		if(plan.MaxLinkCount < dto.UserLinks)
			throw new Exception("Link limit exceeded");

		if(plan.MaxLinkLifetime < dto.LinkLifetime)
			throw new Exception("Link lifetime limit exceeded");

		return true;
	}
}

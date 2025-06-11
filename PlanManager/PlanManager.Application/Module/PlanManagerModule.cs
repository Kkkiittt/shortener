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

	public async Task<bool> CheckPlanCreateAsync(PlanWriteCheckDto dto)
	{
		try
		{
			return await _planService.CheckPlanCreateAsync(dto);
		}
		catch
		{
			return false;
		}
	}

	public async Task<bool> CheckPlanDeleteAsync(long id)
	{
		try
		{
			return await _planService.CheckPlanDeleteAsync(id);
		}
		catch
		{
			return false;
		}
	}

	public async Task<bool> CheckPlanInformateAsync(long id)
	{
		try
		{
			return await _planService.CheckPlanInformateAsync(id);
		}
		catch
		{
			return false;
		}
	}

	public async Task<bool> CheckPlanUpdateAsync(PlanWriteCheckDto dto)
	{
		try
		{
			return await _planService.CheckPlanUpdateAsync(dto);
		}
		catch
		{
			return false;
		}
	}
}

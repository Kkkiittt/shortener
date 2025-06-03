using PlanManager.Application.Dtos;

namespace PlanManager.Application.Interfaces.Module;

public interface IPlanManagerModule
{
	public Task<bool> CheckPlan(PlanCheckDto dto);
}

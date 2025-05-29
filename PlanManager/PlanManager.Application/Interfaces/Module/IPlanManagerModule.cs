using PlanManager.Application.Dtos;

namespace PlanManager.Application.Interfaces.Module;

public interface IPlanManagerModule
{
	public bool CheckPlan(PlanCheckDto dto);
}

using PlanManager.Application.Dtos;

namespace PlanManager.Application.Interfaces.Module;

public interface IPlanManagerModule
{
	public Task<bool> CheckPlanCreateAsync(PlanWriteCheckDto dto);

	public Task<bool> CheckPlanUpdateAsync(PlanWriteCheckDto dto);

	public Task<bool> CheckPlanDeleteAsync(long id);

	public Task<bool> CheckPlanInformateAsync(long id);
}

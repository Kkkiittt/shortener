using PlanManager.Application.Dtos;
using PlanManager.Domain.Entities;

namespace PlanManager.Application.Interfaces.Services;

public interface IPlanService
{
	public Task<bool> CheckPlanActionAsync(PlanCheckDto dto);

	public Task<bool> CreatePlanAsync(PlanCreateDto dto);

	public Task<bool> UpdatePlanAsync(PlanUpdateDto dto);

	public Task<bool> DeletePlanAsync(int id);

	public Task<Plan> GetPlanAsync(long id);

	public Task<List<Plan>> GetPlansAsync();

	public Task<List<PlanGetDto>> GetPlansShortAsync();
}

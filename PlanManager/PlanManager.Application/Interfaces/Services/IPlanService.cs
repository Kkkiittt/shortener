using PlanManager.Application.Dtos;
using PlanManager.Domain.Entities;

namespace PlanManager.Application.Interfaces.Services;

public interface IPlanService
{
	public Task<bool> CheckActionAsync(PlanCheckDto dto);

	public Task<bool> CreatePlanAsync(PlanCreateDto dto);

	public Task<bool> UpdatePlanAsync(PlanUpdateDto dto);

	public Task<bool> DeletePlanAsync(int id);

	public Task<Plan> GetPlanAsync(int id);

	public Task<List<Plan>> GetPlansAsync(int page, int pageSize);

	public Task<List<PlanGetDto>> GetPlansShortAsync(int page, int pageSize);
}

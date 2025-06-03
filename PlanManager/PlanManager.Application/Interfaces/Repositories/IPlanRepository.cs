using PlanManager.Domain.Entities;

namespace PlanManager.Application.Interfaces.Repositories;

public interface IPlanRepository
{
	public bool CreatePlan(Plan plan);

	public bool DeletePlan(Plan plan);

	public bool UpdatePlan(Plan plan);

	public Task<Plan?> GetPlanAsync(long id);

	public Task<List<Plan>> GetPlansAsync(int skip, int take);

	public Task<List<Plan>> GetPlansAsync();

	public Task<bool> SaveChangesAsync();
}

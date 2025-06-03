
using Microsoft.EntityFrameworkCore;

using PlanManager.Application.Interfaces.Repositories;
using PlanManager.DataAccess.Contexts;
using PlanManager.Domain.Entities;

namespace PlanManager.DataAccess.Repositories;

public class PlanRepository : IPlanRepository
{
	private readonly PlanDbContext _context;

	public PlanRepository(PlanDbContext context)
	{
		_context = context;
	}

	public bool CreatePlan(Plan plan)
	{
		_context.Add(plan);
		return true;
	}

	public bool DeletePlan(Plan plan)
	{
		_context.Remove(plan);
		return true;
	}

	public bool UpdatePlan(Plan plan)
	{
		_context.Update(plan);
		return true;
	}

	public async Task<Plan?> GetPlanAsync(long id)
	{
		return await _context.Plans.FindAsync(id);
	}

	public async Task<List<Plan>> GetPlansAsync(int skip, int take)
	{
		return await _context.Plans.Skip(skip).Take(take).ToListAsync();
	}

	public async Task<List<Plan>> GetPlansAsync()
	{
		return await _context.Plans.ToListAsync();
	}

	public async Task<bool> SaveChangesAsync()
	{
		return await _context.SaveChangesAsync() > 0;
	}
}

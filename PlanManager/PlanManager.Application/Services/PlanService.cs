
using PlanManager.Application.Dtos;
using PlanManager.Application.Interfaces.Repositories;
using PlanManager.Application.Interfaces.Services;
using PlanManager.Domain.Entities;
using PlanManager.Domain.Enums;

using Shared.Interfaces;

namespace PlanManager.Application.Services;

public class PlanService : IPlanService
{
	private readonly IPlanRepository _planRepo;
	private readonly IUserIdentifier _user;

	public PlanService(IPlanRepository planRepository, IUserIdentifier user)
	{
		_planRepo = planRepository;
		_user = user;
	}

	public async Task<bool> CheckPlanActionAsync(PlanCheckDto dto)
	{
		Plan? plan = await _planRepo.GetPlanAsync(dto.Id);

		if(plan == null)
			throw new Exception("Plan not found");

		if(!plan.Actions.Contains(dto.Action))
			throw new Exception("Action unavailable");
		if(dto.Action == PlanAction.LinkCreate)
		{
			if(plan.MaxLinkCount <= dto.UserLinks)
				throw new Exception("Link limit exceeded");

			if(plan.MaxLinkLifetime < dto.LinkLifetime)
				throw new Exception("Link lifetime limit exceeded");
		}

		if(dto.Action == PlanAction.LinkUpdate)
		{
			if(plan.MaxLinkLifetime < dto.LinkLifetime)
				throw new Exception("Link lifetime limit exceeded");
		}

		return true;
	}

	public async Task<bool> CreatePlanAsync(PlanCreateDto dto)
	{
		if(!_user.IsAdmin)
			throw new Exception("Access denied");

		Plan plan = new Plan(dto.Name, dto.MaxLinkLifetime, dto.MaxLinkCount, dto.Actions, dto.Description)
		{
			Created = DateTime.UtcNow,
		};

		_planRepo.CreatePlan(plan);
		return await _planRepo.SaveChangesAsync();
	}

	public async Task<bool> DeletePlanAsync(int id)
	{
		if(!_user.IsAdmin)
			throw new Exception("Access denied");

		Plan? plan = await _planRepo.GetPlanAsync(id);

		if(plan == null)
			throw new Exception("Plan not found");

		_planRepo.DeletePlan(plan);
		return await _planRepo.SaveChangesAsync();
	}

	public async Task<Plan> GetPlanAsync(long id)
	{
		if(!_user.IsAdmin)
			throw new Exception("Access denied");

		Plan? plan = await _planRepo.GetPlanAsync(id);

		if(plan == null)
			throw new Exception("Plan not found");

		return plan;
	}

	public async Task<List<Plan>> GetPlansAsync()
	{
		if(!_user.IsAdmin)
			throw new Exception("Access denied");

		return await _planRepo.GetPlansAsync();
	}

	public async Task<List<PlanGetDto>> GetPlansShortAsync()
	{
		return (await _planRepo.GetPlansAsync()).Select(p => (PlanGetDto)p).ToList();
	}

	public async Task<bool> UpdatePlanAsync(PlanUpdateDto dto)
	{
		if(!_user.IsAdmin)
			throw new Exception("Access denied");

		Plan? plan = await _planRepo.GetPlanAsync(dto.Id);

		if(plan == null)
			throw new Exception("Plan not found");

		plan.Actions = dto.Actions;
		plan.Description = dto.Description ?? "";
		plan.MaxLinkCount = dto.MaxLinkCount;
		plan.MaxLinkLifetime = dto.MaxLinkLifetime;
		plan.Name = dto.Name;
		plan.Updated = DateTime.UtcNow;

		_planRepo.UpdatePlan(plan);
		return await _planRepo.SaveChangesAsync();
	}
}

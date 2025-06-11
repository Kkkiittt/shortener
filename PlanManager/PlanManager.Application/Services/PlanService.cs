
using PlanManager.Application.Dtos;
using PlanManager.Application.Interfaces.Repositories;
using PlanManager.Application.Interfaces.Services;
using PlanManager.Domain.Entities;

using Shortener.Shared.Enums;
using Shortener.Shared.Exceptions;
using Shortener.Shared.Interfaces;

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

	public async Task<bool> CheckPlanCreateAsync(PlanWriteCheckDto dto)
	{
		Plan? plan = await _planRepo.GetPlanAsync(dto.Id);

		if(plan == null)
			throw new ShortenerNotFoundException("Plan not found", "Plan", dto.Id.ToString());

		if(!plan.Actions.Contains(ClientAction.LinkCreate))
			throw new ShortenerPermissionException("Action not allowed", "PlanActions");

		if(plan.MaxLinkCount <= dto.UserLinks)
			throw new ShortenerPermissionException("Max link count exceeded", "Plan maximum link count");

		if(plan.MaxLinkLifetime < dto.LinkLifetime)
			throw new ShortenerPermissionException("Max link lifetime exceeded", "Plan maximum link lifetime");

		return true;
	}

	public async Task<bool> CheckPlanDeleteAsync(long id)
	{
		Plan? plan = await _planRepo.GetPlanAsync(id);

		if(plan == null)
			throw new ShortenerNotFoundException("Plan not found", "Plan", id.ToString());

		if(!plan.Actions.Contains(ClientAction.LinkDelete))
			throw new ShortenerPermissionException("Action not allowed", "PlanActions");

		return true;
	}

	public async Task<bool> CheckPlanInformateAsync(long id)
	{
		Plan? plan = await _planRepo.GetPlanAsync(id);

		if(plan == null)
			throw new ShortenerNotFoundException("Plan not found", "Plan", id.ToString());

		if(!plan.Actions.Contains(ClientAction.LinkInfo))
			throw new ShortenerPermissionException("Action not allowed", "PlanActions");

		return true;
	}

	public async Task<bool> CheckPlanUpdateAsync(PlanWriteCheckDto dto)
	{
		Plan? plan = await _planRepo.GetPlanAsync(dto.Id);

		if(plan == null)
			throw new ShortenerNotFoundException("Plan not found", "Plan", dto.Id.ToString());

		if(!plan.Actions.Contains(ClientAction.LinkUpdate))
			throw new ShortenerPermissionException("Action not allowed", "Plan Actions");

		if(plan.MaxLinkLifetime < dto.LinkLifetime)
			throw new ShortenerPermissionException("Max link lifetime exceeded", "Plan maximum link lifetime");

		return true;
	}

	public async Task<bool> CreatePlanAsync(PlanCreateDto dto)
	{
		if(!_user.IsAdmin)
			throw new ShortenerPermissionException("Access denied", "User role");

		Plan plan = new Plan(dto.Name, dto.MaxLinkLifetime, dto.MaxLinkCount, dto.Actions, dto.Description ?? "", dto.Cost, dto.SubscriptionPeriod ?? TimeSpan.MaxValue)
		{
			Created = DateTime.UtcNow,
		};

		_planRepo.CreatePlan(plan);
		return await _planRepo.SaveChangesAsync();
	}

	public async Task<bool> DeletePlanAsync(int id)
	{
		if(!_user.IsAdmin)
			throw new ShortenerPermissionException("Access denied", "User role");

		Plan? plan = await _planRepo.GetPlanAsync(id);

		if(plan == null)
			throw new ShortenerNotFoundException("Plan not found", "Plan", id.ToString());

		_planRepo.DeletePlan(plan);
		return await _planRepo.SaveChangesAsync();
	}

	public async Task<Plan> GetPlanAsync(long id)
	{
		if(!_user.IsAdmin)
			throw new ShortenerPermissionException("Access denied", "User role");

		Plan? plan = await _planRepo.GetPlanAsync(id);

		if(plan == null)
			throw new ShortenerNotFoundException("Plan not found", "Plan", id.ToString());

		return plan;
	}

	public async Task<List<Plan>> GetPlansAsync()
	{
		if(!_user.IsAdmin)
			throw new ShortenerPermissionException("Access denied", "User role");

		return await _planRepo.GetPlansAsync();
	}

	public async Task<List<PlanGetDto>> GetPlansShortAsync()
	{
		return (await _planRepo.GetPlansAsync()).Select(p => (PlanGetDto)p).ToList();
	}

	public async Task<bool> UpdatePlanAsync(PlanUpdateDto dto)
	{
		if(!_user.IsAdmin)
			throw new ShortenerPermissionException("Access denied", "User role");

		Plan? plan = await _planRepo.GetPlanAsync(dto.Id);

		if(plan == null)
			throw new ShortenerNotFoundException("Plan not found", "Plan", dto.Id.ToString());

		plan.Actions = dto.Actions;
		plan.Description = dto.Description ?? "";
		plan.MaxLinkCount = dto.MaxLinkCount;
		plan.MaxLinkLifetime = dto.MaxLinkLifetime;
		plan.Name = dto.Name;
		plan.Updated = DateTime.UtcNow;
		plan.Cost = dto.Cost;
		plan.SubscriptionPeriod = dto.SubscriptionPeriod ?? TimeSpan.MaxValue;//so if no subscription period, then payment is one-time

		_planRepo.UpdatePlan(plan);
		return await _planRepo.SaveChangesAsync();
	}
}

using LinkManager.Application.Dtos;
using LinkManager.Application.Interfaces.Services;

using PlanManager.Application.Dtos;
using PlanManager.Application.Interfaces.Module;

namespace LinkManager.Infrastructure.Services;

public class ClientValidator : IClientValidator
{
	private readonly IPlanManagerModule _plan;

	public ClientValidator(IPlanManagerModule plan)
	{
		_plan = plan;
	}

	public async Task<bool> ValidateAsync(ClientCheckDto dto)
	{
		PlanCheckDto planDto = new PlanCheckDto(dto.PlanId, dto.LinkCount, dto.LinkLifetime, dto.Action);
		return await _plan.CheckPlanAsync(planDto);
	}
}

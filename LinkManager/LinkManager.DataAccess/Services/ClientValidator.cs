using LinkManager.Application.Dtos;
using LinkManager.Application.Interfaces.Services;

using PlanManager.Application.Dtos;
using PlanManager.Application.Interfaces.Module;
using PlanManager.Domain.Enums;

namespace LinkManager.DataAccess.Services;

public class ClientValidator : IClientValidator
{
	private readonly IPlanManagerModule _plan;

	public ClientValidator(IPlanManagerModule plan)
	{
		_plan = plan;
	}

	public async Task<bool> ValidateAsync(ClientCheckDto dto)
	{
		PlanAction action = Enum.Parse<PlanAction>(dto.Action.ToString());
		PlanCheckDto planDto = new PlanCheckDto(dto.PlanId, dto.LinkCount, dto.LinkLifetime, action);
		return await _plan.CheckPlanAsync(planDto);
	}
}

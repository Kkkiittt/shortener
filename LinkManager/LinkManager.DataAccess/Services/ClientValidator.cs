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

	public bool Validate(ClientCheckDto dto)
	{
		PlanAction action = (PlanAction)Enum.Parse(typeof(PlanAction), dto.Action.ToString());
		PlanCheckDto planDto = new PlanCheckDto(dto.PlanId, dto.LinkCount, dto.LinkLifetime, action);
		return _plan.CheckPlan(planDto);
	}
}

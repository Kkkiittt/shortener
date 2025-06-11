
using LinkManager.Application.Dtos;
using LinkManager.Application.Interfaces.Services;

using PlanManager.Application.Dtos;
using PlanManager.Application.Interfaces.Module;

namespace LinkManager.Infrastructure.Services;

public class ClientValidator : IClientValidator
{
	private readonly IPlanManagerModule _planModule;

	public ClientValidator(IPlanManagerModule planModule)
	{
		_planModule = planModule;
	}

	public async Task<bool> ValidateCreateAsync(ClientWriteCheckDto dto)
	{
		PlanWriteCheckDto planDto = new PlanWriteCheckDto(dto.PlanId, dto.LinkCount, dto.LinkLifetime);
		return await _planModule.CheckPlanCreateAsync(planDto);
	}

	public async Task<bool> ValidateDeleteAsync(long id)
	{
		return await _planModule.CheckPlanDeleteAsync(id);
	}

	public async Task<bool> ValidateInformateAsync(long id)
	{
		return await _planModule.CheckPlanInformateAsync(id);
	}

	public async Task<bool> ValidateUpdateAsync(ClientWriteCheckDto dto)
	{
		PlanWriteCheckDto planDto = new PlanWriteCheckDto(dto.PlanId, dto.LinkCount, dto.LinkLifetime);
		return await _planModule.CheckPlanUpdateAsync(planDto);
	}
}

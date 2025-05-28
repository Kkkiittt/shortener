using LinkManager.Application.Dtos;

namespace LinkManager.Application.Interfaces.Services;

public interface IClientValidator
{
	public bool Validate(PlanCheckDto dto);
}



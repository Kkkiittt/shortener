using LinkManager.Application.Interfaces.Dtos;

namespace LinkManager.Application.Interfaces.Services;


public interface IClientValidator
{
	public bool Validate(ClientCheckDto dto);
}



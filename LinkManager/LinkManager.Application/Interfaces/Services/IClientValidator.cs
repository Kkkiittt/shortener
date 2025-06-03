using LinkManager.Application.Dtos;

namespace LinkManager.Application.Interfaces.Services;


public interface IClientValidator
{
	public Task<bool> ValidateAsync(ClientCheckDto dto);
}



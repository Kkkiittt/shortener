using LinkManager.Application.Dtos;

namespace LinkManager.Application.Interfaces.Services;


public interface IClientValidator
{
	public Task<bool> ValidateCreateAsync(ClientWriteCheckDto dto);

	public Task<bool> ValidateUpdateAsync(ClientWriteCheckDto dto);

	public Task<bool> ValidateDeleteAsync(long id);

	public Task<bool> ValidateInformateAsync(long id);
}



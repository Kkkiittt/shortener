using Authenticator.Application.Dtos;

namespace Authenticator.Application.Interfaces.Services;

public interface IUserManager
{
	public Task<bool> UpdateAsync(UserUpdateDto userDto);

	public Task<long> CreateAsync(UserCreateDto userDto);

	public Task<bool> DeleteAsync(long id);

	public Task<UserGetDto> GetUserAsync(long id);

	public Task<List<UserGetDto>> GetUsersAsync(int page, int pageSize);

	public Task<bool> PromoteAsync(long id);

	public Task<bool> DemoteAsync(long id);

	public Task<bool> SubscribeAsync(long subscription_id = 0);

	public Task<string> LoginAsync(UserLoginDto userDto);
}

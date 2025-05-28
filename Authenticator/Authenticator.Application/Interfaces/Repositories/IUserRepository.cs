using Authenticator.Domain.Entities;

namespace Authenticator.Application.Interfaces.Repositories;

public interface IUserRepository
{
	public long CreateUser(User user);

	public bool UpdateUser(User user);

	public bool DeleteUser(User user);

	public Task<User?> GetUserAsync(long id);

	public Task<User?> GetUserAsync(string email);

	public Task<bool> AnyUserAsync(string email);

	public Task<bool> AnyUserAsync(long id);

	public Task<List<User>> GetUsersAsync(int skip, int take);

	public Task<bool> SaveChangesAsync();
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Authenticator.Domain.Entities;

namespace Authenticator.Application.Interfaces.Repositories;

public interface IUserRepository
{
	public Task<long> CreateUserAsync(User user);

	public Task<bool> UpdateUserAsync(User user);

	public Task<User> GetUserAsync(long id);

	public Task<User> GetUserAsync(string email);

	public Task<bool> DeleteUserAsync(long id);

	public Task<List<User>> GetUsers(int skip, int take);
}

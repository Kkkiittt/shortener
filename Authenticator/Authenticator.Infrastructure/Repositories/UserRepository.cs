using Authenticator.Application.Interfaces.Repositories;
using Authenticator.Domain.Entities;
using Authenticator.Infrastructure.Contexts;

using Microsoft.EntityFrameworkCore;

namespace Authenticator.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
	private readonly UserDbContext _context;

	public UserRepository(UserDbContext context)
	{
		_context = context;
	}

	public long CreateUser(User user)
	{
		_context.Add(user);
		return user.Id;
	}

	public bool DeleteUser(User user)
	{
		_context.Remove(user);
		return true;
	}

	public async Task<User?> GetUserAsync(long id)
	{
		return await _context.Users.FindAsync(id);
	}

	public async Task<User?> GetUserAsync(string email)
	{
		return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
	}

	public async Task<List<User>> GetUsersAsync(int skip, int take)
	{
		return await _context.Users.Skip(skip).Take(take).ToListAsync();
	}

	public bool UpdateUser(User user)
	{
		_context.Update(user);
		return true;
	}

	public async Task<bool> AnyUserAsync(string email)
	{
		return await _context.Users.AnyAsync(x => x.Email == email);
	}

	public async Task<bool> AnyUserAsync(long id)
	{
		return await _context.Users.FindAsync(id) != null;
	}

	public async Task<bool> SaveChangesAsync()
	{
		return await _context.SaveChangesAsync() > 0;
	}
}

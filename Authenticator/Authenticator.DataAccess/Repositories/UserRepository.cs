using Authenticator.Application.Interfaces.Repositories;
using Authenticator.DataAccess.Contexts;
using Authenticator.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Authenticator.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
	private readonly UserDbContext _context;

	public UserRepository(UserDbContext context)
	{
		_context = context;
	}

	public async Task<long> CreateUserAsync(User user)
	{
		_context.Add(user);
		await _context.SaveChangesAsync();
		user = await _context.Users.FirstAsync(x => x.Email == user.Email);
		return user.Id;
	}

	public async Task<bool> DeleteUserAsync(long id)
	{
		_context.Remove(id);
		return await _context.SaveChangesAsync() > 0;
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

	public async Task<bool> UpdateUserAsync(User user)
	{
		_context.Update(user);
		return await _context.SaveChangesAsync() > 0;
	}

	public async Task<bool> AnyUserAsync(string email)
	{
		return await _context.Users.AnyAsync(x => x.Email == email);
	}

	public async Task<bool> AnyUserAsync(long id)
	{
		return await _context.Users.FindAsync(id) != null;
	}
}

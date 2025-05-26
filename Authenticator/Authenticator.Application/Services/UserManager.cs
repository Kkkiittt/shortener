
using System.Security.Claims;

using Authenticator.Application.Dtos;
using Authenticator.Application.Helpers;
using Authenticator.Application.Interfaces.Repositories;
using Authenticator.Application.Interfaces.Services;
using Authenticator.Domain.Entities;

using Microsoft.AspNetCore.Http;

namespace Authenticator.Application.Services;

public class UserManager : IUserManager
{
	public IUserRepository _repo;
	public ITokenGenerator _token;
	public IUserIdentifier _identity;

	public UserManager(IUserRepository repo, ITokenGenerator token, IUserIdentifier identity)
	{
		_token = token;
		_repo = repo;
		_identity = identity;
	}

	public async Task<long> CreateAsync(UserCreateDto userDto)
	{
		if(await _repo.AnyUserAsync(userDto.Email))
		{
			throw new Exception("User already exists");
		}
		User user = new(userDto.Email, Hasher.Hash(userDto.Password), userDto.Name)
		{
			Balance = 0,
			Created = DateTime.Now,
			Role = 1,
			Updated = DateTime.Now
		};
		return await _repo.CreateUserAsync(user);
	}

	public async Task<bool> DeleteAsync(long id)
	{

		if(_identity.Id != id && !_identity.Admin)
		{
			throw new Exception("Access denied");
		}
		User user = await _repo.GetUserAsync(id);
		if(user == null)
			throw new Exception("User not found");
		if(user.Role == 0 || user.Role == 2)
			throw new Exception("Access denied");
		return await _repo.DeleteUserAsync(id);
	}

	public async Task<UserGetDto> GetUserAsync(long id)
	{
		if(_identity.Id != id && !_identity.Admin)
		{
			throw new Exception("Access denied");
		}
		User user = await _repo.GetUserAsync(id);
		if(user == null)
			throw new Exception("User not found");
		return (UserGetDto)user;
	}

	public async Task<List<UserGetDto>> GetUsersAsync(int page, int pageSize)
	{
		if(!_identity.Admin)
			throw new Exception("Access denied");
		int skip = (page - 1) * pageSize;
		List<User> users = await _repo.GetUsersAsync(skip, pageSize);
		return users.Select(x => (UserGetDto)x).ToList();
	}

	public async Task<string> LoginAsync(UserLoginDto userDto)
	{
		User user = await _repo.GetUserAsync(userDto.Email);
		if(Hasher.Verify(userDto.Password, user.PasswordHash))
		{
			string token = _token.GenerateToken(user);
			return token;
		}
		throw new Exception("Invalid credentials");
	}

	public async Task<bool> PromoteAsync(long id)
	{
		if(_identity.Role != 0)
			throw new Exception("Access denied");
		User user = await _repo.GetUserAsync(id);
		user.Role = 2;
		return await _repo.UpdateUserAsync(user);
	}

	public async Task<bool> DemoteAsync(long id)
	{
		if(_identity.Role != 0)
			throw new Exception("Access denied");
		User user = await _repo.GetUserAsync(id);
		user.Role = 1;
		return await _repo.UpdateUserAsync(user);
	}

	public Task<bool> SubscribeAsync(long subscription_id = 0)
	{
		throw new NotImplementedException();
	}

	public Task<bool> UpdateAsync(UserUpdateDto userDto)
	{
		throw new NotImplementedException();
	}
}

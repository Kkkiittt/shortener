
using System.Security.Claims;

using Authenticator.Application.Dtos;
using Authenticator.Application.Helpers;
using Authenticator.Application.Interfaces.Repositories;
using Authenticator.Application.Interfaces.Services;
using Authenticator.Domain.Entities;
using Authenticator.Domain.Enums;

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
			Role = Roles.User,
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
		User? user = await _repo.GetUserAsync(id);
		if(user == null)
			throw new Exception("User not found");
		if(user.Role == Roles.Admin || user.Role == Roles.Owner)
			throw new Exception("Access denied");
		return await _repo.DeleteUserAsync(id);
	}

	public async Task<UserGetDto> GetUserAsync(long id)
	{
		if(_identity.Id != id && !_identity.Admin)
		{
			throw new Exception("Access denied");
		}
		User? user = await _repo.GetUserAsync(id);
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
		User? user = await _repo.GetUserAsync(userDto.Email);
		if(user == null)
			throw new Exception("Invalid credentials");
		if(Hasher.Verify(userDto.Password, user.PasswordHash))
		{
			string token = _token.GenerateToken(user);
			return token;
		}
		throw new Exception("Invalid credentials");
	}

	public async Task<bool> PromoteAsync(long id)
	{
		if(_identity.Role != Roles.Owner)
			throw new Exception("Access denied");
		User? user = await _repo.GetUserAsync(id);
		if(user == null)
			throw new Exception("User not found");
		user.Role = Roles.Admin;
		return await _repo.UpdateUserAsync(user);
	}

	public async Task<bool> DemoteAsync(long id)
	{
		if(_identity.Role != Roles.Owner)
			throw new Exception("Access denied");
		User? user = await _repo.GetUserAsync(id);
		if(user == null)
			throw new Exception("User not found");
		user.Role = Roles.User;
		return await _repo.UpdateUserAsync(user);
	}

	public async Task<bool> SubscribeAsync(long subscriptionId = 0)
	{
		User? user = await _repo.GetUserAsync(_identity.Id);
		if(user == null)
			throw new Exception("User not found");
		user.SubscriptionId = subscriptionId;
		return await _repo.UpdateUserAsync(user);
	}

	public async Task<bool> UpdateAsync(UserUpdateDto userDto)
	{
		if(await _repo.AnyUserAsync(userDto.Email))
		{
			throw new Exception("Email already exists");
		}
		if(!await _repo.AnyUserAsync(userDto.Id))
		{
			throw new Exception("User not found");
		}
		User user = new(userDto.Email, Hasher.Hash(userDto.Password), userDto.Name)
		{
			Updated = DateTime.Now,
			Id = _identity.Id
		};
		return await _repo.UpdateUserAsync(user);
	}
}

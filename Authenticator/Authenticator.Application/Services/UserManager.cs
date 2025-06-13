using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Authenticator.Application.Dtos;
using Authenticator.Application.Interfaces.Repositories;
using Authenticator.Application.Interfaces.Services;
using Authenticator.Domain.Entities;

using Shortener.Shared.Enums;
using Shortener.Shared.Exceptions;
using Shortener.Shared.Helpers;
using Shortener.Shared.Interfaces;


namespace Authenticator.Application.Services;

public class UserManager : IUserManager
{
	public IUserRepository _repo;
	public ITokenManager _token;
	public IUserIdentifier _identity;

	public UserManager(IUserRepository repo, ITokenManager token, IUserIdentifier identity)
	{
		_token = token;
		_repo = repo;
		_identity = identity;
	}

	public async Task<bool> CreateAsync(UserCreateDto userDto)
	{
		if(await _repo.AnyUserAsync(userDto.Email))
			throw new ShortenerUsedException("Email already exists", "Email");

		User user = new(userDto.Email, Hasher.Hash(userDto.Password), userDto.Name);

		_repo.CreateUser(user);
		return await _repo.SaveChangesAsync();
	}

	public async Task<UserUpdateDto> GetTemplateAsync()
	{
		User? user = await _repo.GetUserAsync(_identity.Id);

		if(user == null)
			throw new ShortenerNotFoundException("User not found", "User", _identity.Id.ToString());

		UserUpdateDto result = new(user.Id, user.Email, "", user.Name);
		return result;
	}

	public async Task<bool> DeleteAsync(long id)
	{

		if(_identity.Id != id && !_identity.IsAdmin)
			throw new ShortenerPermissionException("Access denied", "User role");

		User? user = await _repo.GetUserAsync(id);

		if(user == null)
			throw new ShortenerNotFoundException("User not found", "User", id.ToString());

		if(user.Role == Roles.Admin || user.Role == Roles.Owner)
			throw new ShortenerPermissionException("Permission denied", "Object role");

		_repo.DeleteUser(user);
		return await _repo.SaveChangesAsync();
	}

	public async Task<UserGetDto> GetUserAsync(long id)
	{
		if(_identity.Id != id && !_identity.IsAdmin)
			throw new ShortenerPermissionException("Access denied", "User role");

		User? user = await _repo.GetUserAsync(id);

		if(user == null)
			throw new ShortenerNotFoundException("User not found", "User", id.ToString());

		return (UserGetDto)user;
	}

	public async Task<List<UserGetDto>> GetUsersAsync(int page, int pageSize)
	{
		if(!_identity.IsAdmin)
			throw new ShortenerPermissionException("Access denied", "User role");

		if(pageSize < 1)
			throw new ShortenerArgumentException("Invalid page size", "Page size");

		if(page < 1)
			throw new ShortenerArgumentException("Invalid page", "Page");

		if(pageSize > 50)
			pageSize = 50;

		int skip = (page - 1) * pageSize;
		List<User> users = await _repo.GetUsersAsync(skip, pageSize);
		return users.Select(x => (UserGetDto)x).ToList();
	}

	public async Task<string> LoginAsync(UserLoginDto userDto)
	{
		User? user = await _repo.GetUserAsync(userDto.Email);

		if(user == null)
			throw new ShortenerArgumentException("Invalid credentials", "Email or password");

		if(!Hasher.Verify(userDto.Password, user.PasswordHash))
			throw new ShortenerArgumentException("Invalid credentials", "Email or password");

		return _token.GenerateToken(user);
	}

	public async Task<bool> PromoteAsync(long id)
	{
		if(_identity.Role != Roles.Owner)
			throw new ShortenerPermissionException("Access denied", "User role");

		User? user = await _repo.GetUserAsync(id);

		if(user == null)
			throw new ShortenerNotFoundException("User not found", "User", id.ToString());

		if(user.Role != Roles.Owner)
		{
			user.Role = Roles.Admin;
			user.SubscriptionId = 2;
		}

		_repo.UpdateUser(user);
		return await _repo.SaveChangesAsync();
	}

	public async Task<bool> DemoteAsync(long id)
	{
		if(_identity.Role != Roles.Owner)
			throw new ShortenerPermissionException("Access denied", "User role");

		User? user = await _repo.GetUserAsync(id);

		if(user == null)
			throw new ShortenerNotFoundException("User not found", "User", id.ToString());

		if(user.Role != Roles.Owner)
		{
			user.Role = Roles.User;
			user.SubscriptionId = 1;
		}

		_repo.UpdateUser(user);
		return await _repo.SaveChangesAsync();
	}

	public async Task<bool> SubscribeAsync(long subscriptionId = 0)
	{
		User? user = await _repo.GetUserAsync(_identity.Id);

		if(user == null)
			throw new ShortenerNotFoundException("User not found", "User", _identity.Id.ToString());

		user.SubscriptionId = subscriptionId;

		_repo.UpdateUser(user);
		return await _repo.SaveChangesAsync();
	}

	public async Task<bool> UpdateAsync(UserUpdateDto userDto)
	{
		User? emailUser = await _repo.GetUserAsync(userDto.Email);
		if(emailUser != null && emailUser.Id != _identity.Id)
			throw new ShortenerUsedException("Email already exists", "Email");

		User? user = await _repo.GetUserAsync(_identity.Id);
		if(user == null)
			throw new ShortenerNotFoundException("User not found", "User", _identity.Id.ToString());

		user.Email = userDto.Email;
		user.Name = userDto.Name;
		user.Updated = DateTime.UtcNow;
		if(userDto.Password != "")
			user.PasswordHash = Hasher.Hash(userDto.Password);

		_repo.UpdateUser(user);
		return await _repo.SaveChangesAsync();
	}

	public async Task<string> RefreshTokenAsync(string token)
	{
		long id = _token.GetIdFromToken(token);
		DateTime issueDate = _token.GetIssueDateFromToken(token);

		User? user = await _repo.GetUserAsync(id);
		if(user == null)
			throw new ShortenerNotFoundException("User not found", "User", id.ToString());

		if(user.Updated >= issueDate)
			throw new ShortenerArgumentException("User updated, relogin", "Token");

		return _token.GenerateToken(user);
	}
}

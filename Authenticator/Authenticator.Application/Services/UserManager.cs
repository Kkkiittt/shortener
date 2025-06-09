
using Authenticator.Application.Dtos;
using Authenticator.Application.Interfaces.Repositories;
using Authenticator.Application.Interfaces.Services;
using Authenticator.Domain.Entities;

using Shared.Enums;
using Shared.Helpers;
using Shared.Interfaces;


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

	public async Task<bool> CreateAsync(UserCreateDto userDto)
	{
		if(await _repo.AnyUserAsync(userDto.Email))
			throw new Exception("Email already exists");

		User user = new(userDto.Email, Hasher.Hash(userDto.Password), userDto.Name)
		{
			Balance = 0,
			Created = DateTime.UtcNow,
			Role = Roles.User,
			Updated = DateTime.UtcNow
		};

		var res = _repo.CreateUser(user);
		return await _repo.SaveChangesAsync();
	}

	public async Task<UserUpdateDto> GetTemplateAsync()
	{
		User? user = await _repo.GetUserAsync(_identity.Id);

		if(user == null)
			throw new Exception("User not found");

		UserUpdateDto result = new(user.Id, user.Email, "", user.Name);
		return result;
	}

	public async Task<bool> DeleteAsync(long id)
	{

		if(_identity.Id != id && !_identity.IsAdmin)
			throw new Exception("Access denied");

		User? user = await _repo.GetUserAsync(id);

		if(user == null)
			throw new Exception("User not found");

		if(user.Role == Roles.Admin || user.Role == Roles.Owner)
			throw new Exception("Access denied");

		_repo.DeleteUser(user);
		return await _repo.SaveChangesAsync();
	}

	public async Task<UserGetDto> GetUserAsync(long id)
	{
		if(_identity.Id != id && !_identity.IsAdmin)
			throw new Exception("Access denied");

		User? user = await _repo.GetUserAsync(id);

		if(user == null)
			throw new Exception("User not found");

		return (UserGetDto)user;
	}

	public async Task<List<UserGetDto>> GetUsersAsync(int page, int pageSize)
	{
		if(!_identity.IsAdmin)
			throw new Exception("Access denied");

		if(pageSize < 1 || page < 1)
			throw new ArgumentException("Invalid arguments");

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
			throw new Exception("Invalid credentials");

		if(Hasher.Verify(userDto.Password, user.PasswordHash))
			return _token.GenerateToken(user);

		throw new Exception("Invalid credentials");
	}

	public async Task<bool> PromoteAsync(long id)
	{
		if(_identity.Role != Roles.Owner)
			throw new Exception("Access denied");

		User? user = await _repo.GetUserAsync(id);

		if(user == null)
			throw new Exception("User not found");

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
			throw new Exception("Access denied");

		User? user = await _repo.GetUserAsync(id);

		if(user == null)
			throw new Exception("User not found");

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
			throw new Exception("User not found");

		user.SubscriptionId = subscriptionId;
		_repo.UpdateUser(user);
		return await _repo.SaveChangesAsync();
	}

	public async Task<bool> UpdateAsync(UserUpdateDto userDto)
	{
		User? emailUser = await _repo.GetUserAsync(userDto.Email);
		if(emailUser != null && emailUser.Id != _identity.Id)
			throw new Exception("Email already exists");

		User? user = await _repo.GetUserAsync(_identity.Id);
		if(user == null)
			throw new Exception("User not found");

		user.Email = userDto.Email;
		user.Name = userDto.Name;
		user.Updated = DateTime.UtcNow;
		if(userDto.Password != "")
			user.PasswordHash = Hasher.Hash(userDto.Password);

		_repo.UpdateUser(user);
		return await _repo.SaveChangesAsync();
	}
}

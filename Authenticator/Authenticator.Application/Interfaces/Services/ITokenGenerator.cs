using Authenticator.Domain.Entities;

namespace Authenticator.Application.Interfaces.Services;

public interface ITokenGenerator
{
	public string GenerateToken(User user);
}

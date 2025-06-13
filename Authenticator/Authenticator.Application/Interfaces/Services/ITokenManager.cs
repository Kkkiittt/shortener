using Authenticator.Domain.Entities;

namespace Authenticator.Application.Interfaces.Services;

public interface ITokenManager
{
	public string GenerateToken(User user);

	public long GetIdFromToken(string token);

	public DateTime GetIssueDateFromToken(string token);
}

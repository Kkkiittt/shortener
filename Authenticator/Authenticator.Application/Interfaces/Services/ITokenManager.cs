using Authenticator.Domain.Entities;

namespace Authenticator.Application.Interfaces.Services;

public interface ITokenManager
{
	public string GenerateToken(User user);

	public long GetId(string token);

	public DateTime GetIssueDate(string token);
}

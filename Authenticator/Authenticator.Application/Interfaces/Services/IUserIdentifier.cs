namespace Authenticator.Application.Interfaces.Services;

public interface IUserIdentifier
{
	public long Id { get; }
	public string Email { get; }
	public long Role { get; }
	public bool Admin { get; }
}

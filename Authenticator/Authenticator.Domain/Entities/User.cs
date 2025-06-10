using Shortener.Shared.Enums;

namespace Authenticator.Domain.Entities;
public class User
{
	public long Id { get; set; }
	public string Email { get; set; }
	public string PasswordHash { get; set; }
	public string Name { get; set; }
	public DateTime Created { get; set; }
	public DateTime Updated { get; set; }
	public Roles Role { get; set; }
	public long SubscriptionId { get; set; }
	public double Balance { get; set; }

	public User(string email, string passwordHash, string name)
	{
		Email = email;
		PasswordHash = passwordHash;
		Name = name;
		Created = DateTime.Now;
		Updated = DateTime.Now;
		Role = Roles.User;
		SubscriptionId = 1;
		Balance = 0;
	}
}

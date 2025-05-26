using System.ComponentModel.DataAnnotations;

using Authenticator.Domain.Enums;

using Microsoft.EntityFrameworkCore;
namespace Authenticator.Domain.Entities;
[Index(nameof(Email), IsUnique = true)]
public class User
{
	[Key]
	public long Id { get; set; }
	[EmailAddress, Required]
	public string Email { get; set; }
	public string PasswordHash { get; set; }
	[Length(maximumLength: 30, minimumLength: 3), Required]
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
		SubscriptionId = 0;
	}
}

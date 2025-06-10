using Authenticator.Domain.Entities;

using Shortener.Shared.Enums;

namespace Authenticator.Application.Dtos;

public class UserGetDto
{
	public long Id { get; set; }
	public string Email { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public DateTime Created { get; set; }
	public DateTime Updated { get; set; }
	public Roles Role { get; set; }
	public long SubscriptionId { get; set; }
	public double Balance { get; set; }

	public static explicit operator UserGetDto(User user)
	{
		return new UserGetDto { Id = user.Id, Email = user.Email, Name = user.Name, Created = user.Created, Updated = user.Updated, Role = user.Role, Balance = user.Balance, SubscriptionId = user.SubscriptionId };
	}
}

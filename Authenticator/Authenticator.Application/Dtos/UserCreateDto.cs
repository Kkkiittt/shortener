using System.ComponentModel.DataAnnotations;

namespace Authenticator.Application.Dtos;

public class UserCreateDto
{
	[EmailAddress]
	public string Email { get; set; }
	[Length(8, 30)]
	public string Password { get; set; }
	[Length(2, 30)]
	public string Name { get; set; }

	public UserCreateDto(string email, string password, string name)
	{
		Email = email;
		Password = password;
		Name = name;
	}
}

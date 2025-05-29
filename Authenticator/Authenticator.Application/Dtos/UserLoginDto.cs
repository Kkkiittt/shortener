using System.ComponentModel.DataAnnotations;

namespace Authenticator.Application.Dtos;

public class UserLoginDto
{
	[EmailAddress]
	public string Email { get; set; }
	[Length(8, 30)]
	public string Password { get; set; }

	public UserLoginDto(string email, string password)
	{
		Email = email;
		Password = password;
	}
}

using System.ComponentModel.DataAnnotations;

namespace Authenticator.Application.Dtos;

public class UserUpdateDto
{
	[Required]
	public long Id { get; set; }
	[EmailAddress]
	public string Email { get; set; }
	public string Password { get; set; }
	[Length(2, 30)]
	public string Name { get; set; }
		
	public UserUpdateDto(long id, string email, string password, string name)
	{
		if((password.Length < 8 || password.Length > 30) && password != "")
			throw new Shortener.Shared.Exceptions.ShortenerArgumentException("Password must be between 8 and 30 symbols", "Password");
		Id = id;
		Email = email;
		Password = password;
		Name = name;
	}
}

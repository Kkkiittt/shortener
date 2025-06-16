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
		Id = id;
		Email = email;
		Password = password;
		Name = name;
	}
}

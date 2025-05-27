namespace Authenticator.Application.Dtos;

public class UserCreateDto
{
	public string Email { get; set; }
	public string Password { get; set; }
	public string Name { get; set; }

	public UserCreateDto(string email, string password, string name)
	{
		Email = email;
		Password = password;
		Name = name;
	}
}

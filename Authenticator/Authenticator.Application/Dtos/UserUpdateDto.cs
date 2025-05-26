namespace Authenticator.Application.Dtos;
public class UserUpdateDto : UserCreateDto
{
	public long? Id { get; set; }

	public UserUpdateDto(long id, string email, string password, string name) : base(email, password, name)
	{
		Id = id;
	}
}

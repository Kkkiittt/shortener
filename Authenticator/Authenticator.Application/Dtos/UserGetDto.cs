namespace Authenticator.Application.Dtos;

public class UserGetDto
{
	public long Id { get; set; }
	public string Email { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public DateTime Created { get; set; }
	public DateTime Updated { get; set; }
	public long Role { get; set; }
	public double Balance { get; set; }
}

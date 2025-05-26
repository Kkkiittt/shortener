namespace Authenticator.Application.Helpers;

public static class PasswordHasher
{
	public static string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

	public static bool Verify(string password, string hashedPassword) => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}

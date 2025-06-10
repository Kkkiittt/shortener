namespace Shortener.Shared.Helpers;

public static class Hasher
{
	public static string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

	public static bool Verify(string password, string hashedPassword) => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}

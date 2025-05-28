namespace LinkManager.Application.Helpers;

public class Hasher
{
	public static string Hash(string text)
	{
		return BCrypt.Net.BCrypt.HashPassword(text);
	}

	public static bool Verify(string text, string hash)
	{
		return BCrypt.Net.BCrypt.Verify(text, hash);
	}
}

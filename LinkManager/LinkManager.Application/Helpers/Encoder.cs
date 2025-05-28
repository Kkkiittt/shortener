using System.Text;

namespace LinkManager.Application.Helpers;

public static class Encoder
{
	public static string Encode(string plainText)
	{
		return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
	}

	public static string Decode(string encodedData)
	{
		return Encoding.UTF8.GetString(Convert.FromBase64String(encodedData));
	}
}

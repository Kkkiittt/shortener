namespace Shortener.Shared.Helpers;

public static class Encoder
{
	public static string Encode(long id)
	{
		return Convert.ToBase64String(BitConverter.GetBytes(id));
	}

	public static long Decode(string encodedData)
	{
		return BitConverter.ToInt64(Convert.FromBase64String(encodedData));
	}
}

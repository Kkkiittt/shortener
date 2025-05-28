namespace LinkManager.Application.Dtos;

public class LinkUpdateDto
{
	public string ShortLink { get; set; }
	public string LongLink { get; set; }
	public string? Password { get; set; }
	public int Lifetime { get; set; }

	public LinkUpdateDto(string shortLink, string longLink, string? password, int lifetime)
	{
		ShortLink = shortLink;
		LongLink = longLink;
		Password = password;
		Lifetime = lifetime;
	}
}

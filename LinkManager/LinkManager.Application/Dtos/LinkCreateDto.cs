using System.ComponentModel.DataAnnotations;

namespace LinkManager.Application.Dtos;

public class LinkCreateDto
{
	[Required]
	public string LongLink { get; set; }
	public string? Password { get; set; }
	[Required]
	public int Lifetime { get; set; }

	public LinkCreateDto(string longLink, string? password, int lifetime)
	{
		LongLink = longLink;
		Password = password;
		Lifetime = lifetime;
	}
}

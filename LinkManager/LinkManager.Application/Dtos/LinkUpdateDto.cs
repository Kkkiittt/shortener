using System.ComponentModel.DataAnnotations;

namespace LinkManager.Application.Dtos;

public class LinkUpdateDto : LinkCreateDto
{
	[Required]
	public string ShortLink { get; set; }

	public LinkUpdateDto(string shortLink, string longLink, string? password, int lifetime) : base(longLink, password, lifetime)
	{
		ShortLink = shortLink;
	}
}

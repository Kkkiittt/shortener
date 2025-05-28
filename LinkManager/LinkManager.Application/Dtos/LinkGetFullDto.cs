using LinkManager.Domain.Entities;

namespace LinkManager.Application.Dtos;

public class LinkGetFullDto
{
	public long Id { get; set; }
	public long UserId { get; set; }
	public string Url { get; set; } = string.Empty;
	public DateTime Created { get; set; }
	public DateTime Updated { get; set; }
	public int Clicks { get; set; }
	public int LifeTime { get; set; }
	public bool HasPassword { get; set; }

	public static explicit operator LinkGetFullDto(Link link)
	{
		return new LinkGetFullDto
		{
			Id = link.Id,
			UserId = link.UserId,
			Url = link.Url,
			Created = link.Created,
			Updated = link.Updated,
			Clicks = link.Clicks,
			LifeTime = link.LifeTime,
			HasPassword = link.PasswordHash != null
		};
	}
}

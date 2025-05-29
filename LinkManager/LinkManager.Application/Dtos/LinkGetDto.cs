namespace LinkManager.Application.Dtos;

public class LinkGetDto
{
	public string ShortLink { get; set; } = string.Empty;
	public string Url { get; set; } = string.Empty;
	public int Clicks { get; set; }
	public int Lifetime { get; set; }
	public DateTime Created { get; set; }
	public bool HasPassword { get; set; }
}

namespace LinkManager.Domain.Entities;

public class Link
{
	public long Id { get; set; }
	public string Url { get; set; }
	public DateTime Created { get; set; }
	public DateTime Updated { get; set; }
	public string? PasswordHash { get; set; }
	public int Clicks { get; set; }
	public int LifeTime { get; set; }

	public Link(string url, int lifeTime, string? passwordHash = null)
	{
		Url = url;
		Created = DateTime.UtcNow;
		Updated = DateTime.UtcNow;
		PasswordHash = passwordHash;
		Clicks = 0;
		LifeTime = lifeTime;
	}
}

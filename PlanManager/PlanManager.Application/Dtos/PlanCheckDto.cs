namespace PlanManager.Application.Dtos;

public class PlanWriteCheckDto
{
	public long Id { get; set; }
	public int UserLinks { get; set; }
	public int LinkLifetime { get; set; }

	public PlanWriteCheckDto(long id, int userLinks, int linkLifetime)
	{
		Id = id;
		UserLinks = userLinks;
		LinkLifetime = linkLifetime;
	}
}

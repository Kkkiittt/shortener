namespace LinkManager.Application.Dtos;

public class ClientWriteCheckDto
{
	public long PlanId { get; set; }
	public int LinkCount { get; set; }
	public int LinkLifetime { get; set; }

	public ClientWriteCheckDto(long planId, int linkCount, int linkLifetime)
	{
		PlanId = planId;
		LinkCount = linkCount;
		LinkLifetime = linkLifetime;
	}
}

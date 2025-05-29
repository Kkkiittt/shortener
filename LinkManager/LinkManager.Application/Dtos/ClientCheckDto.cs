using LinkManager.Domain.Enums;

namespace LinkManager.Application.Dtos;

public class ClientCheckDto
{
	public long PlanId { get; set; }
	public int LinkCount { get; set; }
	public int LinkLifetime { get; set; }
	public ClientAction Action { get; set; }

	public ClientCheckDto(long planId, int linkCount, int linkLifetime, ClientAction action)
	{
		PlanId = planId;
		LinkCount = linkCount;
		LinkLifetime = linkLifetime;
		Action = action;
	}
}

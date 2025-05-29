using PlanManager.Domain.Enums;

namespace PlanManager.Application.Dtos;

public class PlanCheckDto
{
	public long Id { get; set; }
	public int UserLinks { get; set; }
	public int LinkLifetime { get; set; }
	public PlanAction Action { get; set; }

	public PlanCheckDto(long id, int userLinks, int linkLifetime, PlanAction action)
	{
		Id = id;
		UserLinks = userLinks;
		LinkLifetime = linkLifetime;
		Action = action;
	}
}

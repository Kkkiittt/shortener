using PlanManager.Domain.Enums;

namespace PlanManager.Application.Dtos;

public class PlanCreateDto
{
	public string Name { get; set; }
	public string? Description { get; set; }
	public List<PlanAction> Actions { get; set; }
	public int MaxLinkCount { get; set; }
	public int MaxLinkLifetime { get; set; }

	public PlanCreateDto(string name, List<PlanAction> actions, int maxLinkCount, int maxLinkLifetime, string? description=null)
	{
		Name = name;
		Description = description;
		Actions = actions;
		MaxLinkCount = maxLinkCount;
		MaxLinkLifetime = maxLinkLifetime;
	}
}

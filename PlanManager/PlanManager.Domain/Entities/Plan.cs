using PlanManager.Domain.Enums;

namespace PlanManager.Domain.Entities;

public class Plan
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public DateTime Created { get; set; }
	public DateTime Updated { get; set; }
	public List<PlanAction> Actions { get; set; } = new();
	public int MaxLinkLifetime { get; set; }
	public int MaxLinkCount { get; set; }

	public Plan(string name, int maxLifetime, int maxLinks, List<PlanAction> actions)
	{
		Name = name;
		MaxLinkLifetime = maxLifetime;
		MaxLinkCount = maxLinks;
		Actions = actions;
		Description = string.Empty;
	}
}

using Shortener.Shared.Enums;

namespace PlanManager.Domain.Entities;

public class Plan
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public DateTime Created { get; set; }
	public DateTime Updated { get; set; }
	public List<ClientAction> Actions { get; set; } = new();
	public int MaxLinkLifetime { get; set; }
	public int MaxLinkCount { get; set; }
	public double Cost { get; set; }
	public TimeSpan SubscriptionPeriod { get; set; }

	public Plan(string name, int maxLinkLifetime, int maxLinkCount, List<ClientAction> actions, string description, double cost, TimeSpan subscriptionPeriod)
	{
		Name = name;
		MaxLinkLifetime = maxLinkLifetime;
		MaxLinkCount = maxLinkCount;
		Actions = actions;
		Description = description;
		Cost = cost;
		SubscriptionPeriod = subscriptionPeriod;
	}
}

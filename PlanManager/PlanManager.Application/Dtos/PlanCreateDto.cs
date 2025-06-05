using System.ComponentModel.DataAnnotations;

using Shared.Enums;

namespace PlanManager.Application.Dtos;

public class PlanCreateDto
{
	[Length(2, 30)]
	public string Name { get; set; }
	public string? Description { get; set; }
	[Required]
	public List<ClientAction> Actions { get; set; }
	[Required]
	public int MaxLinkCount { get; set; }
	[Required]
	public int MaxLinkLifetime { get; set; }
	[Required]
	public double Cost { get; set; }
	public TimeSpan? SubscriptionPeriod { get; set; }

	public PlanCreateDto(string name, List<ClientAction> actions, int maxLinkCount, int maxLinkLifetime, string? description = null, double cost = 0, TimeSpan? subscriptionPeriod = null)
	{
		Name = name;
		Description = description;
		Actions = actions;
		MaxLinkCount = maxLinkCount;
		MaxLinkLifetime = maxLinkLifetime;
		Cost = cost;
		SubscriptionPeriod = subscriptionPeriod;
	}
}

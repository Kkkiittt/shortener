using PlanManager.Domain.Entities;

namespace PlanManager.Application.Dtos;

public class PlanGetDto
{
	public long Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public List<string> Actions { get; set; } = new();
	public int MaxLinkCount { get; set; }
	public int MaxLinkLifetime { get; set; }
	public double Cost { get; set; }
	public TimeSpan SubscriptionPeriod { get; set; }

	public static explicit operator PlanGetDto(Plan plan)
	{
		return new PlanGetDto
		{
			Name = plan.Name,
			Description = plan.Description,
			Actions = plan.Actions.Select(x => x.ToString()).ToList(),
			MaxLinkCount = plan.MaxLinkCount,
			MaxLinkLifetime = plan.MaxLinkLifetime,
			Id = plan.Id,
			Cost = plan.Cost,
			SubscriptionPeriod = plan.SubscriptionPeriod
		};
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlanManager.Domain.Entities;
using PlanManager.Domain.Enums;

namespace PlanManager.Application.Dtos;

public class PlanGetDto
{
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public List<PlanAction> Actions { get; set; } = new();
	public int MaxLinkCount { get; set; }
	public int MaxLinkLifetime { get; set; }

	public static explicit operator PlanGetDto(Plan plan)
	{
		return new PlanGetDto
		{
			Name = plan.Name,
			Description = plan.Description,
			Actions = plan.Actions,
			MaxLinkCount = plan.MaxLinkCount,
			MaxLinkLifetime = plan.MaxLinkLifetime
		};
	}
}

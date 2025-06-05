
using System.ComponentModel.DataAnnotations;

using Shared.Enums;

namespace PlanManager.Application.Dtos;

public class PlanUpdateDto : PlanCreateDto
{
	[Required]
	public long Id { get; set; }
	public PlanUpdateDto(long id, string name, List<ClientAction> actions, int maxLinkCount, int maxLinkLifetime, string? description = null) : base(name, actions, maxLinkCount, maxLinkLifetime, description)
	{
		Id = id;
	}
}

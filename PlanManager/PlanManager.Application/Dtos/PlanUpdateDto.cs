
using PlanManager.Domain.Enums;

namespace PlanManager.Application.Dtos;

public class PlanUpdateDto : PlanCreateDto
{
	public long Id { get; set; }
	public PlanUpdateDto(long id, string name, List<PlanAction> actions, int maxLinkCount, int maxLinkLifetime, string? description = null) : base(name, actions, maxLinkCount, maxLinkLifetime, description)
	{
		Id = id;
	}
}

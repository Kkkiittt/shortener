using LinkManager.Domain.Enums;

namespace LinkManager.Application.Dtos;

public class ClientCheckDto
{
	public long PlanId { get; set; }
	public int ClientLinks { get; set; }
	public int Lifetime { get; set; }
	public ClientAction Action { get; set; }

	public ClientCheckDto()
	{
		throw new NotImplementedException();
	}
}

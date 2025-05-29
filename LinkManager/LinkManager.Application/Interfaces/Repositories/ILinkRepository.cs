using LinkManager.Domain.Entities;

namespace LinkManager.Application.Interfaces.Repositories;

public interface ILinkRepository
{
	public bool CreateLink(Link link);

	public bool UpdateLink(Link link);

	public bool DeleteLink(Link link);

	public Task<Link?> GetLinkAsync(long id);

	public Task<List<Link>> GetLinksAsync(int skip, int take);

	public Task<List<Link>> GetLinksAsync(long userId);

	public Task<int> GetLinkCountAsync(long userId);

	public Task<bool> SaveChangesAsync();
}


using LinkManager.Application.Interfaces.Repositories;
using LinkManager.DataAccess.Contexts;
using LinkManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace LinkManager.DataAccess.Repositories;

public class LinkRepository : ILinkRepository
{

	private readonly LinkDbContext _context;

	public LinkRepository(LinkDbContext context)
	{
		_context = context;
	}

	public bool CreateLink(Link link)
	{
		_context.Add(link);
		return true;
	}

	public bool UpdateLink(Link link)
	{
		_context.Update(link);
		return true;
	}

	public bool DeleteLink(Link link)
	{
		_context.Remove(link);
		return true;
	}

	public async Task<Link?> GetLinkAsync(long id)
	{
		return await _context.Links.FindAsync(id);
	}

	public async Task<List<Link>> GetLinksAsync(int skip, int take)
	{
		return await _context.Links.Skip(skip).Take(take).ToListAsync();
	}

	public async Task<List<Link>> GetLinksAsync(long userId)
	{
		return await _context.Links.Where(x => x.UserId == userId).ToListAsync();
	}

	public async Task<bool> SaveChangesAsync()
	{
		return await _context.SaveChangesAsync() > 0;
	}
}

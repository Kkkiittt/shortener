using LinkManager.DataAccess.Configurations;
using LinkManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace LinkManager.DataAccess.Contexts;

public class LinkDbContext : DbContext
{
	private readonly IEntityTypeConfiguration<Link> _config;

	public DbSet<Link> Links { get; protected set; } = null!;

	public LinkDbContext(DbContextOptions<LinkDbContext> options) : base(options)
	{
		_config = new LinkEntityTypeConfiguration();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(_config);
		base.OnModelCreating(modelBuilder);
	}
}

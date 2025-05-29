using LinkManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace LinkManager.DataAccess.Contexts;

public class LinkDbContext : DbContext
{
	public readonly IEntityTypeConfiguration<Link> _config;

	public DbSet<Link> Links { get; protected set; } = null!;

	public LinkDbContext(DbContextOptions<LinkDbContext> options, IEntityTypeConfiguration<Link> config) : base(options)
	{
		_config = config;
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(_config);
		base.OnModelCreating(modelBuilder);
	}
}

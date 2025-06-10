using Microsoft.EntityFrameworkCore;

using PlanManager.Infrastructure.Configurations;
using PlanManager.Domain.Entities;

namespace PlanManager.Infrastructure.Contexts;

public class PlanDbContext : DbContext
{
	private readonly IEntityTypeConfiguration<Plan> _config;

	public DbSet<Plan> Plans { get; protected set; } = null!;

	public PlanDbContext(DbContextOptions<PlanDbContext> options) : base(options)
	{
		_config = new PlanEntityTypeConfiguration();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfiguration(_config);
	}
}

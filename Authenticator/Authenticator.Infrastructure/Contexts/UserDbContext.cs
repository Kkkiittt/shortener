using Authenticator.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Authenticator.Infrastructure.Contexts;

public class UserDbContext : DbContext
{
	private readonly IEntityTypeConfiguration<User> _userConfig;

	public DbSet<User> Users { get; protected set; } = null!;

	public UserDbContext(DbContextOptions<UserDbContext> options, IEntityTypeConfiguration<User> userConfig) : base(options)
	{
		_userConfig = userConfig;
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(_userConfig);
		base.OnModelCreating(modelBuilder);
	}
}

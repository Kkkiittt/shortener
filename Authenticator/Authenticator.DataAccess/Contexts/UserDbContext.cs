using System.Globalization;

using Authenticator.Application.Helpers;
using Authenticator.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Authenticator.DataAccess.Contexts;

public class UserDbContext : DbContext
{
	private readonly IConfiguration _config;

	public DbSet<User> Users { get; set; } = null!;

	public UserDbContext(DbContextOptions<UserDbContext> options, IConfiguration config) : base(options)
	{
		_config = config;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql(_config.GetConnectionString("Database"));
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		var admConfig = _config.GetSection("Admin");
		User admin = new User(admConfig["Email"], Hasher.Hash(admConfig["Password"]), admConfig["Name"]);
		admin.Created = DateTime.ParseExact(admConfig["Created"], "dd/mm/yyyy", CultureInfo.InvariantCulture);
		admin.Balance = int.Parse(admConfig["Balance"]);
		admin.Role = int.Parse(admConfig["Role"]);
		admin.Id = int.Parse(admConfig["Id"]);
		modelBuilder.Entity<User>().HasData(admin);

		base.OnModelCreating(modelBuilder);
	}
}

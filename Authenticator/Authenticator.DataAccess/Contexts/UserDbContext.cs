using System.Globalization;

using Authenticator.Application.Helpers;
using Authenticator.Domain.Entities;
using Authenticator.Domain.Enums;

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
		string? email = admConfig["Email"],
			password = admConfig["Password"],
			name = admConfig["Name"],
			created = admConfig["Created"],
			balance = admConfig["Balance"],
			role = admConfig["Role"],
			id = admConfig["Id"];
		if(email == null || password == null || name == null || created == null || balance == null || role == null || id == null)
			throw new Exception("Admin configuration is not set.");
		User admin = new User(email, Hasher.Hash(password), name);
		admin.Created = DateTime.ParseExact(created, "dd/mm/yyyy", CultureInfo.InvariantCulture);
		admin.Balance = int.Parse(balance);
		admin.Role = (Roles)int.Parse(role);
		admin.Id = int.Parse(id);
		modelBuilder.Entity<User>().HasData(admin);

		base.OnModelCreating(modelBuilder);
	}
}

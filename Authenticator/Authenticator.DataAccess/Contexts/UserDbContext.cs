using System.Globalization;

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
		{
			throw new Exception("Admin settings not found");
		}
		var admin = new User(email, password, name)
		{
			Created = DateTime.ParseExact(created, "dd/MM/yyyy", CultureInfo.InvariantCulture),
			Balance = int.Parse(balance),
			Role = (Roles)int.Parse(role),
			Id = int.Parse(id),
			Updated = DateTime.ParseExact(created, "dd/MM/yyyy", CultureInfo.InvariantCulture)
		};
		modelBuilder.Entity<User>().HasData(admin);
		modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
		modelBuilder.Entity<User>().HasKey(x => x.Id);
		base.OnModelCreating(modelBuilder);
	}
}

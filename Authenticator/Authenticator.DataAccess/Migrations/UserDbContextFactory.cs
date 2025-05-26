using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Authenticator.Application.Helpers;
using Authenticator.DataAccess.Contexts;
using Authenticator.Domain.Entities;
using Authenticator.Domain.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Authenticator.DataAccess.Migrations;

public class UserDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
{
	public UserDbContext CreateDbContext(string[] args)
	{
		// Build configuration manually
		var basePath = AppContext.BaseDirectory;
		var configPath = Path.Combine(basePath, "..", "..", "..", "..", "Authenticator.Api");

		IConfigurationRoot configuration = new ConfigurationBuilder()
			.SetBasePath(configPath)
			.AddJsonFile("appsettings.secure.json")
			.Build();

		var builder = new DbContextOptionsBuilder<UserDbContext>();
		var connectionString = configuration.GetConnectionString("Database");

		builder.UseNpgsql(connectionString);

		var context = new UserDbContext(builder.Options, configuration);
		//var admConfig = configuration.GetSection("Admin");

		//string? email = admConfig["Email"],
		//	password = admConfig["Password"],
		//	name = admConfig["Name"],
		//	created = admConfig["Created"],
		//	balance = admConfig["Balance"],
		//	role = admConfig["Role"],
		//	id = admConfig["Id"];
		//if(email == null || password == null || name == null || created == null || balance == null || role == null || id == null)
		//{
		//	throw new Exception("Admin settings not found");
		//}
		//var admin = new User(
		//		email,
		//		Hasher.Hash(password),
		//		name
		//	)
		//{
		//	Created = DateTime.ParseExact(created, "dd/MM/yyyy", CultureInfo.InvariantCulture),
		//	Balance = int.Parse(balance),
		//	Role = (Roles)int.Parse(role),
		//	Id = int.Parse(id)
		//};
		//var model = context.Model;
		//var entityType = model.FindEntityType(typeof(User));

		//var modelbuilder = new ModelBuilder();

		//modelbuilder.Entity<User>(b => { b.HasData(admin); });


		return context;
	}
}

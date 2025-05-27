using Authenticator.DataAccess.Contexts;

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


		return context;
	}
}

using Authenticator.DataAccess.Configurations;
using Authenticator.DataAccess.Contexts;
using Authenticator.Domain.Entities;

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

		IConfigurationRoot config = new ConfigurationBuilder()
			.SetBasePath(configPath)
			.AddJsonFile("appsettings.secure.json")
			.Build();

		IEntityTypeConfiguration<User> userConfig = new UserEntityTypeConfiguration(config);

		var builder = new DbContextOptionsBuilder<UserDbContext>();
		var connectionString = config.GetConnectionString("Database");

		builder.UseNpgsql(connectionString);


		var context = new UserDbContext(builder.Options, userConfig);


		return context;
	}
}

using System.Globalization;

using Authenticator.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

using Shortener.Shared.Enums;

namespace Authenticator.Infrastructure.Configurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
	private readonly IConfiguration _config;

	public UserEntityTypeConfiguration(IConfiguration config)
	{
		_config = config;
	}

	public void Configure(EntityTypeBuilder<User> userBuilder)
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
			throw new InvalidOperationException("Admin settings not found");
		}
		var admin = new User(email, password, name)
		{
			Created = DateTime.ParseExact(created, "dd/MM/yyyy", CultureInfo.InvariantCulture),
			Balance = int.Parse(balance),
			Role = (Roles)int.Parse(role),
			Id = int.Parse(id),
			Updated = DateTime.ParseExact(created, "dd/MM/yyyy", CultureInfo.InvariantCulture)
		};

		userBuilder.HasData(admin);
		userBuilder.HasIndex(x => x.Email).IsUnique();
		userBuilder.HasKey(x => x.Id);
	}
}

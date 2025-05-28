using System.Globalization;

using Authenticator.Domain.Entities;
using Authenticator.Domain.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace Authenticator.DataAccess.Configurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
	private readonly IConfiguration _config;

	public UserEntityTypeConfiguration(IConfiguration config)
	{
		_config = config;
	}

	public void Configure(EntityTypeBuilder<User> builder)
	{

		builder.HasKey(x => x.Id);

		builder.HasIndex(x => x.Email)
			.IsUnique();
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
		builder.HasData(admin);
		builder.HasIndex(x => x.Email).IsUnique();
		builder.HasKey(x => x.Id);
	}
}

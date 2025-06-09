using Authenticator.Application.Interfaces.Repositories;
using Authenticator.Application.Interfaces.Services;
using Authenticator.Application.Services;
using Authenticator.DataAccess.Configurations;
using Authenticator.DataAccess.Contexts;
using Authenticator.DataAccess.Repositories;
using Authenticator.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Authenticator.Api.Services;

public static class AuthenticatorModuleExtensions
{
	public static IServiceCollection AddAuthenticatorModule(this IServiceCollection services, IConfiguration config)
	{
		services.AddScoped<IUserManager, UserManager>();
		services.AddScoped<ITokenGenerator, TokenGenerator>();
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IEntityTypeConfiguration<User>, UserEntityTypeConfiguration>();
		services.AddDbContext<UserDbContext>(options =>
		{
			var connect = config.GetConnectionString("UserDb");
			options.UseNpgsql(connect);
		});
		services.AddMvc().AddApplicationPart(typeof(AuthenticatorModuleExtensions).Assembly).AddControllersAsServices();
		return services;
	}
}

using Authenticator.Application.Interfaces.Repositories;
using Authenticator.Application.Interfaces.Services;
using Authenticator.Application.Services;
using Authenticator.Domain.Entities;
using Authenticator.Infrastructure.Configurations;
using Authenticator.Infrastructure.Contexts;
using Authenticator.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authenticator.Infrastructure.Extensions;

public static class AuthenticatorModuleExtensions
{
	public static IServiceCollection AddAuthenticatorModule(this IServiceCollection services, IConfiguration config)
	{
		services.AddScoped<IUserManager, UserManager>();
		services.AddScoped<ITokenManager, TokenManager>();
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IEntityTypeConfiguration<User>, UserEntityTypeConfiguration>();
		services.AddDbContext<UserDbContext>(options =>
		{
			var connect = config.GetConnectionString("UserDb");
			options.UseNpgsql(connect);
		});
		return services;
	}
}

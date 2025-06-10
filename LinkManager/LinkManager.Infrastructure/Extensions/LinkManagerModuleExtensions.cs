using LinkManager.Application.Interfaces.Repositories;
using LinkManager.Application.Interfaces.Services;
using LinkManager.Application.Services;
using LinkManager.Domain.Entities;
using LinkManager.Infrastructure.Configurations;
using LinkManager.Infrastructure.Contexts;
using LinkManager.Infrastructure.Repositories;
using LinkManager.Infrastructure.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkManager.Infrastructure.Extensions;

public static class LinkManagerModuleExtensions
{
	public static IServiceCollection AddLinkManagerModule(this IServiceCollection services, IConfiguration config)
	{
		services.AddScoped<ILinkRepository, LinkRepository>();
		services.AddScoped<IClientValidator, ClientValidator>();
		services.AddScoped<ILinkService, LinkService>();
		services.AddScoped<IEntityTypeConfiguration<Link>, LinkEntityTypeConfiguration>();
		services.AddDbContext<LinkDbContext>((serv, opt) =>
		{
			opt.UseNpgsql(config.GetConnectionString("LinkDb"));
		});
		services.AddMvc().AddApplicationPart(typeof(LinkManagerModuleExtensions).Assembly).AddControllersAsServices();
		return services;
	}
}

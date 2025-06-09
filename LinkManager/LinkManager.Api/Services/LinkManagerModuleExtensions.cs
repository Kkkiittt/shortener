using LinkManager.Application.Interfaces.Repositories;
using LinkManager.Application.Interfaces.Services;
using LinkManager.Application.Services;
using LinkManager.DataAccess.Configurations;
using LinkManager.DataAccess.Contexts;
using LinkManager.DataAccess.Repositories;
using LinkManager.DataAccess.Services;
using LinkManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using Shared.Interfaces;
using Shared.Services;

namespace LinkManager.Api.Services;

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

using Microsoft.EntityFrameworkCore;

using PlanManager.Application.Interfaces.Module;
using PlanManager.Application.Interfaces.Repositories;
using PlanManager.Application.Interfaces.Services;
using PlanManager.Application.Module;
using PlanManager.Application.Services;
using PlanManager.DataAccess.Contexts;
using PlanManager.DataAccess.Repositories;

namespace PlanManager.Api.Services;

public static class PlanManagerModuleExtension
{
	public static IServiceCollection AddPlanManagerModule(this IServiceCollection services, IConfiguration config)
	{
		services.AddDbContext<PlanDbContext>(options =>
		{
			options.UseNpgsql(config.GetConnectionString("PlanDb"));
		});
		services.AddScoped<IPlanRepository, PlanRepository>();
		services.AddScoped<IPlanService, PlanService>();
		services.AddScoped<IPlanManagerModule, PlanManagerModule>();
		services.AddMvc().AddApplicationPart(typeof(PlanManagerModuleExtension).Assembly).AddControllersAsServices();
		return services;
	}
}

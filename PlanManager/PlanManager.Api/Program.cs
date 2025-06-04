
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using PlanManager.Application.Interfaces.Module;
using PlanManager.Application.Interfaces.Repositories;
using PlanManager.Application.Interfaces.Services;
using PlanManager.Application.Module;
using PlanManager.Application.Services;
using PlanManager.DataAccess.Contexts;
using PlanManager.DataAccess.Repositories;

using Shared.Interfaces;
using Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<IPlanManagerModule, PlanManagerModule>();
builder.Services.AddScoped<IUserIdentifier, UserIdentifier>();
builder.Services.AddEndpointsApiExplorer();
builder.Configuration.AddJsonFile("appsettings.secure.json");
builder.Services.AddJwtBearerAuthentication(builder.Configuration, true);
builder.Services.AddDbContext<PlanDbContext>(opt =>
{
	var connection = builder.Configuration.GetConnectionString("Database");
	opt.UseNpgsql(connection);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();


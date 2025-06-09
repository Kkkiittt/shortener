using LinkManager.Application.Interfaces.Repositories;
using LinkManager.Application.Interfaces.Services;
using LinkManager.Application.Services;
using LinkManager.DataAccess.Configurations;
using LinkManager.DataAccess.Contexts;
using LinkManager.DataAccess.Repositories;
using LinkManager.DataAccess.Services;
using LinkManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using PlanManager.Api.Services;

using Shared.Interfaces;
using Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Configuration.AddJsonFile("appsettings.secure.json");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddJwtBearerAuthentication(builder.Configuration, true);

builder.Services.AddScoped<ILinkRepository, LinkRepository>();
builder.Services.AddScoped<IClientValidator, ClientValidator>();
builder.Services.AddScoped<ILinkService, LinkService>();
builder.Services.AddScoped<IUserIdentifier, UserIdentifier>();
builder.Services.AddScoped<IEntityTypeConfiguration<Link>, LinkEntityTypeConfiguration>();
builder.Services.AddDbContext<LinkDbContext>((serv, opt) =>
{
	var config = serv.GetRequiredService<IConfiguration>();
	opt.UseNpgsql(config.GetConnectionString("LinkDb"));
});

builder.Services.AddPlanManagerModule(builder.Configuration);

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

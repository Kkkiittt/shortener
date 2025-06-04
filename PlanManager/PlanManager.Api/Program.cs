
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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Configuration.AddJsonFile("appsettings.secure.json");
builder.Services.AddSwaggerGen(c =>
{
	c.AddSecurityDefinition("Bearer", new()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme."
	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement {
		{
			new OpenApiSecurityScheme {
				Reference = new OpenApiReference {
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				},
			},
			Array.Empty<string>()
		}
	});
});
var config = builder.Configuration.GetSection("JWT");
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(opt =>
{
	opt.TokenValidationParameters = new()
	{
		ValidAudience = config["Audience"],
		ValidIssuer = config["Issuer"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Secret"] ?? throw new Exception("Secret not found"))),
		ValidateAudience = true,
		ValidateIssuer = true,
		ValidateIssuerSigningKey = true,
		ValidateLifetime = true,
	};
});
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

app.UseAuthorization();


app.MapControllers();

app.Run();


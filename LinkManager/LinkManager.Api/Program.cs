using Microsoft.EntityFrameworkCore;
using LinkManager.DataAccess.Contexts;
using LinkManager.DataAccess.Configurations;
using LinkManager.Domain.Entities;
using LinkManager.DataAccess.Repositories;
using LinkManager.Application.Interfaces.Repositories;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LinkManager.Application.Interfaces.Services;
using LinkManager.Application.Services;
using LinkManager.Api.Services;
using LinkManager.DataAccess.Services;
using PlanManager.DataAccess.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Configuration.AddJsonFile("appsettings.secure.json");
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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
builder.Services.AddScoped<ILinkRepository, LinkRepository>();
builder.Services.AddScoped<IClientValidator, ClientValidator>();
builder.Services.AddScoped<ILinkService, LinkService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserIdentifier, UserIdentifier>();
builder.Services.AddScoped<IEntityTypeConfiguration<Link>, LinkEntityTypeConfiguration>();
builder.Services.AddDbContext<LinkDbContext>((serv, opt) =>
{
	var config = serv.GetRequiredService<IConfiguration>();
	opt.UseNpgsql(config.GetConnectionString("Database"));
});
var config = builder.Configuration.GetSection("JWT");
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
	options.TokenValidationParameters = new()
	{
		ValidateAudience = true,
		ValidAudience = config["Audience"],
		ValidateIssuer = true,
		ValidIssuer = config["Issuer"],
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Secret"] ?? throw new Exception("Key not found"))),
		ValidateLifetime = true
	};
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

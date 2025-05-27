using System.Text;

using Authenticator.Api.Services;
using Authenticator.Application.Interfaces.Repositories;
using Authenticator.Application.Interfaces.Services;
using Authenticator.Application.Services;
using Authenticator.DataAccess.Contexts;
using Authenticator.DataAccess.Repositories;


// Add the following NuGet package to your project if not already installed:
// Microsoft.AspNetCore.Authentication.JwtBearer
// You can do this by running the following command in the terminal:
// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder();
// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.secure.json");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IUserIdentifier, UserIdentifier>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddHttpContextAccessor();
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
				}
			},
			Array.Empty<string>()
		}
	});
});
builder.Services.AddDbContext<UserDbContext>((service, options) =>
{
	var config = service.GetRequiredService<IConfiguration>();
	var connect = config.GetConnectionString("Database");
	options.UseNpgsql(connect);
});
var config = builder.Configuration.GetSection("JWT");
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
builder.Services.AddAuthorization();

//var context = builder.Services.BuildServiceProvider().GetRequiredService<UserDbContext>();
//Console.WriteLine(context.Users.ToList()[0]);
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

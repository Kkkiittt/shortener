using Authenticator.Application.Interfaces.Repositories;
using Authenticator.Application.Interfaces.Services;
using Authenticator.Application.Services;
using Authenticator.DataAccess.Configurations;
using Authenticator.DataAccess.Contexts;
using Authenticator.DataAccess.Repositories;
using Authenticator.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using Shared.Interfaces;
using Shared.Services;



var builder = WebApplication.CreateBuilder();
// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.secure.json");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IUserIdentifier, UserIdentifier>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEntityTypeConfiguration<User>, UserEntityTypeConfiguration>();
builder.Services.AddDbContext<UserDbContext>((service, options) =>
{
	var config = service.GetRequiredService<IConfiguration>();
	var connect = config.GetConnectionString("Database");
	options.UseNpgsql(connect);
});
builder.Services.AddJwtBearerAuthentication(builder.Configuration, true);
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

using Authenticator.Infrastructure.Extensions;

using Shortener.Shared.Interfaces;
using Shortener.Shared.Services;



var builder = WebApplication.CreateBuilder();
// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.secure.json");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddJwtBearerAuthentication(builder.Configuration, true);
builder.Services.AddScoped<IUserIdentifier, HttpUserIdentifier>();

builder.Services.AddAuthenticatorModule(builder.Configuration);

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

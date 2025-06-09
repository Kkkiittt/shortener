using Authenticator.DataAccess.Extensions;

using LinkManager.DataAccess.Extensions;

using PlanManager.DataAccess.Extensions;

using Shared.Interfaces;
using Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.secure.json");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddJwtBearerAuthentication(builder.Configuration, true);
builder.Services.AddScoped<IUserIdentifier, UserIdentifier>();

builder.Services.AddPlanManagerModule(builder.Configuration);
builder.Services.AddLinkManagerModule(builder.Configuration);
builder.Services.AddAuthenticatorModule(builder.Configuration);

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

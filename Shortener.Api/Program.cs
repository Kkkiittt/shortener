using Authenticator.Infrastructure.Extensions;

using LinkManager.Infrastructure.Extensions;

using PlanManager.Infrastructure.Extensions;

using Shortener.Api.Middlewares;
using Shortener.Shared.Interfaces;
using Shortener.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.secure.json");

builder.Services.AddControllers(opt =>
{
	opt.Filters.Add<ExceptionFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddJwtBearerAuthentication(builder.Configuration, true);
builder.Services.AddScoped<IUserIdentifier, HttpUserIdentifier>();

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

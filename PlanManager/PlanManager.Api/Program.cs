using PlanManager.DataAccess.Extensions;

using Shared.Interfaces;
using Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Configuration.AddJsonFile("appsettings.secure.json");
builder.Services.AddJwtBearerAuthentication(builder.Configuration, true);

builder.Services.AddPlanManagerModule(builder.Configuration);
builder.Services.AddScoped<IUserIdentifier, UserIdentifier>();

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


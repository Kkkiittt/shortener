
using Microsoft.EntityFrameworkCore;

using PlanManager.DataAccess.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Configuration.AddJsonFile("appsettings.secure.json");
builder.Services.AddSwaggerGen();
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


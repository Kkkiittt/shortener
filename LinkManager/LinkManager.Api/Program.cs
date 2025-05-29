using Microsoft.EntityFrameworkCore;
using LinkManager.DataAccess.Contexts;
using LinkManager.DataAccess.Configurations;
using LinkManager.Domain.Entities;
using LinkManager.DataAccess.Repositories;
using LinkManager.Application.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Configuration.AddJsonFile("appsettings.secure.json");
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ILinkRepository, LinkRepository>();
builder.Services.AddScoped<IEntityTypeConfiguration<Link>, LinkEntityTypeConfiguration>();
builder.Services.AddDbContext<LinkDbContext>((serv, opt) =>
{
	var config = serv.GetRequiredService<IConfiguration>();
	opt.UseNpgsql(config.GetConnectionString("Database"));
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

//using Microsoft.EntityFrameworkCore.Design;
using Authenticator.DataAccess.Contexts;

using Microsoft.EntityFrameworkCore;


namespace Authenticator.Api;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		// Add services to the container.
		builder.Configuration.AddJsonFile("appsettings.secure.json");
		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		builder.Services.AddDbContext<UserDbContext>((service, options) =>
		{
			var config = service.GetRequiredService<IConfiguration>();
			var connect = config.GetConnectionString("Database");
			options.UseNpgsql(connect);
		});

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

		app.UseAuthorization();


		app.MapControllers();

		app.Run();
	}
}

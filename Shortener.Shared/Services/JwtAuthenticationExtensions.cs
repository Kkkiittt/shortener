using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Shortener.Shared.Services;

public static class JwtAuthenticationExtensions
{
	public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services, IConfiguration config, bool swaggerAuth = false)
	{
		if(swaggerAuth)
		{
			services.AddSwaggerGen(c =>
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
		}
		else
		{
			services.AddSwaggerGen();
		}
		config = config.GetSection("JWT");
		services.AddAuthorization();
		services.AddAuthentication("Bearer").AddJwtBearer(opt =>
		{
			opt.TokenValidationParameters = new()
			{
				ValidAudience = config["Audience"],
				ValidIssuer = config["Issuer"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Secret"] ?? throw new Exception("Secret not found"))),
				ValidateAudience = true,
				ValidateIssuer = true,
				ValidateIssuerSigningKey = true,
				ValidateLifetime = true,
			};
		});
		return services;
	}
}

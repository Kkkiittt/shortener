using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Authenticator.Application.Interfaces.Services;
using Authenticator.Domain.Entities;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Authenticator.Application.Services;

public class TokenGenerator : ITokenGenerator
{
	private readonly IConfiguration _config;
	public TokenGenerator(IConfiguration config)
	{
		_config = config;
	}

	public string GenerateToken(User user)
	{
		Claim[] claims =
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Email, user.Email),
			new Claim(ClaimTypes.Role, user.Role.ToString())
		};
		var jwtConfig = _config.GetSection("JWT");
		SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Secret"] ?? throw new ArgumentNullException()));
		SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(issuer: jwtConfig["Issuer"], audience: jwtConfig["Audience"], claims: claims, signingCredentials: credentials);
		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}

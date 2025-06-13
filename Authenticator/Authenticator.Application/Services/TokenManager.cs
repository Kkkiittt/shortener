using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Authenticator.Application.Interfaces.Services;
using Authenticator.Domain.Entities;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Shortener.Shared.Exceptions;

namespace Authenticator.Application.Services;

public class TokenManager : ITokenManager
{
	private readonly IConfiguration _config;

	public TokenManager(IConfiguration config)
	{
		_config = config;
	}

	public string GenerateToken(User user)
	{
		Claim[] claims =
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Email, user.Email),
			new Claim(ClaimTypes.Role, user.Role.ToString()),
			new Claim ("Subscription", user.SubscriptionId.ToString()),
			new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString())
		};
		var jwtConfig = _config.GetSection("JWT");
		SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Secret"] ?? throw new ArgumentNullException("Key not found")));
		SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(issuer: jwtConfig["Issuer"], audience: jwtConfig["Audience"], claims: claims, signingCredentials: credentials, expires: DateTime.Now.AddDays(1));
		return new JwtSecurityTokenHandler().WriteToken(token);
	}

	public long GetId(string token)
	{
		List<Claim> claims = GetClaims(token).ToList();

		long id = long.Parse(claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? throw new ShortenerArgumentException("Invalid token", "Token"));
		return id;
	}

	public DateTime GetIssueDate(string token)
	{
		List<Claim> claims = GetClaims(token).ToList();

		DateTime issueDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Iat)?.Value ?? throw new ShortenerArgumentException("Invalid token", "Token"))).UtcDateTime;
		return issueDate;
	}

	protected IEnumerable<Claim> GetClaims(string token)
	{
		JwtSecurityToken jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
		return jwtToken.Claims;
	}
}

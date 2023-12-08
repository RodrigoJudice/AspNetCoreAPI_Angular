using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService : ITokenService
{
  private readonly IConfiguration _config;
  private readonly SymmetricSecurityKey _key;

  public TokenService(IConfiguration config)
  {
    _config = config;
    _key = new SymmetricSecurityKey(
   Encoding.UTF8.GetBytes(_config["TokenKey"] ?? throw new Exception("Token key not found")));
  }

  public Task<string> CreateToken(AppUser user)
  {
    var claims = new List<Claim>
    {
      new(JwtRegisteredClaimNames.NameId, user.UserName)
    };

    var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.UtcNow.AddDays(7),
      SigningCredentials = creds
    };

    var tokenHandler = new JwtSecurityTokenHandler();

    var token = tokenHandler.CreateToken(tokenDescriptor);

    return Task.FromResult(tokenHandler.WriteToken(token));

  }
}

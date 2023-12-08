namespace API.EndPoints.Account;

using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class Login(DataContext context, ITokenService tokenService) : EndpointBaseAsync
.WithRequest<LoginRequest>
.WithActionResult<LoginResponse>
{
  private readonly DataContext _context = context;
  private readonly ITokenService _tokenService = tokenService;

  [HttpPost("api/accounts/login")]
  public override async Task<ActionResult<LoginResponse>> HandleAsync(LoginRequest request, CancellationToken cancellationToken = default)
  {
    var user = await
    _context.Users.SingleOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);

    if (user == null)
    {
      return Unauthorized("Invalid username");
    }

    using var hmac = new HMACSHA512(user.PasswordSalt);
    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

    for (int i = 0; i < computedHash.Length; i++)
    {
      if (computedHash[i] != user.PasswordHash[i])
      {
        return Unauthorized("Invalid password");
      }
    }
    var token = await _tokenService.CreateToken(user);

    return new LoginResponse
    {
      UserName = user.UserName,
      Token = token
    };
  }
}

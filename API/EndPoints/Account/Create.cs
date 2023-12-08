using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.EndPoints.Account;
public class Create(DataContext context) : EndpointBaseAsync
  .WithRequest<CreateAccountRequest>
  .WithActionResult
{
  private readonly DataContext _context = context;

  [HttpPost("api/accounts")]
  public override async Task<ActionResult> HandleAsync(
      [FromBody] CreateAccountRequest request, CancellationToken cancellationToken = default)
  {
    if (await UserExists(request.UserName))
    {
      return BadRequest("Username is taken");
    }


    using var hmac = new HMACSHA512();
    var user = new AppUser
    {
      UserName = request.UserName.ToLower(),
      PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
      PasswordSalt = hmac.Key
    };

    _context.Users.Add(user);
    await _context.SaveChangesAsync();

    return Created();
  }
  private async Task<bool> UserExists(string username)
  {
#pragma warning disable CA1862 // Use the 'StringComparison' method overloads to perform case-insensitive string comparisons
    return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
#pragma warning restore CA1862 // Use the 'StringComparison' method overloads to perform case-insensitive string comparisons
  }
}

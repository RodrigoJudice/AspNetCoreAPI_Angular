using API.Data;
using API.Entities;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.EndPoints.Users;

public class Get(DataContext context) : EndpointBaseAsync
  .WithRequest<int>
  .WithResult<AppUser>
{
  private readonly DataContext _context = context;

  [Authorize]
  [HttpGet("api/users/{id}")]
  public override async Task<AppUser> HandleAsync(int id, CancellationToken cancellationToken = default)
  {
    return await _context.Users.FindAsync(id) ?? throw new KeyNotFoundException();
  }
}

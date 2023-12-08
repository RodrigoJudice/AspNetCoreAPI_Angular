using API.Data;
using API.Entities;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace API.EndPoints;

public class GetUser : EndpointBaseAsync
  .WithRequest<int>
  .WithResult<AppUser>
{
  private readonly DataContext _context;

  public GetUser(DataContext context)
  {
    _context = context;
  }

  [HttpGet("api/users/{id}")]
  public override async Task<AppUser> HandleAsync(int id, CancellationToken cancellationToken = default)
  {
    return await _context.Users.FindAsync(id) ?? throw new KeyNotFoundException();
  }
}

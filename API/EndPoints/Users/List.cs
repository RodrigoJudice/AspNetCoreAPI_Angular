namespace API.EndPoints.Users;

using API.Data;
using API.Entities;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

public class List(DataContext context) : EndpointBaseAsync
  .WithoutRequest
  .WithResult<IEnumerable<AppUser>>
{
  private readonly DataContext _context = context;

  [Authorize]
  [HttpGet("/api/users")]
  [SwaggerOperation(
          Summary = "List all Authors",
          Description = "List all Authors",
          OperationId = "Author_List",
          Tags = new[] { "AuthorEndpoint" })
      ]
  public override async Task<IEnumerable<AppUser>> HandleAsync(CancellationToken cancellationToken = default)
  {
    return await _context.Users.ToListAsync();
  }
}

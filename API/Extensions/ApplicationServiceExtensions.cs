using System.Text;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;


namespace API.Extensions;

public static class ApplicationServiceExtensions
{
  public static IServiceCollection AddApplicationsServices(this IServiceCollection services, IConfiguration config)
  {
    services.AddDbContext<DataContext>(options =>
    {
      options.UseSqlite(config.GetConnectionString("DefaultConnection"));
    });
    services.AddScoped<ITokenService, TokenService>();

    services.AddCors();
    return services;

  }
}



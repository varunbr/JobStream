using JobStream.Data;
using JobStream.Seed;
using Microsoft.EntityFrameworkCore;

namespace JobStream.Extensions
{
  public static class ApplicationServiceExtensions
  {
    public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
    {
      services.AddDbContext<DataContext>(options =>
      {
        options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
      });

      services.AddScoped<SeedDataService>();

      return services;
    }
  }
}

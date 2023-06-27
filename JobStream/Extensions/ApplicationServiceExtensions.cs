using JobStream.Data;
using JobStream.Seed;
using JobStream.Services;
using Microsoft.EntityFrameworkCore;

namespace JobStream.Extensions
{
  public static class ApplicationServiceExtensions
  {
    public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
    {
      services.AddHostedService<JobStreamHostService>();

      services.AddDbContext<DataContext>(options =>
      {
        options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
      });
      services.AddScoped<SeedDataService>();

      return services;
    }
  }
}

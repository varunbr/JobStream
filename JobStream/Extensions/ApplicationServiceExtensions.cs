using JobStream.Data;
using JobStream.Helpers;
using JobStream.Seed;
using JobStream.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace JobStream.Extensions
{
  public static class ApplicationServiceExtensions
  {
    public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
    {
      services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

      services.AddHostedService<JobStreamHostService>();

      services.AddDbContext<DataContext>(options =>
      {
        options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
      });
      services.AddScoped<JobRunRepository>();
      services.AddScoped<JobProcessRepository>();
      services.AddScoped<JobHistoryRepository>();

      services.AddScoped<SeedDataService>();
      services.AddScoped<JobProcessService>();
      services.AddScoped<JobRunnerService>();
      services.AddScoped<JobHistoryService>();
      services.AddControllers().AddJsonOptions(options =>
      {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
      });

      return services;
    }
  }
}

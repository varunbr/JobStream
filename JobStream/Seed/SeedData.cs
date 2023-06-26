using JobStream.Data;
using JobStream.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobStream.Seed
{
  public class SeedDataService
  {
    private readonly DataContext _context;
    public SeedDataService(DataContext context)
    {
      _context = context;
    }

    public async Task SeedDatabase()
    {
      await _context.Database.MigrateAsync();
    }

    private async Task SeedJobStream()
    {
      _context.JobProcesses.Add(new JobProcess
      {
        Name = "First job",
        Updated = DateTime.UtcNow
      });
    }
  }
}

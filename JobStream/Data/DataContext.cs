using JobStream.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobStream.Data
{
  public class DataContext : DbContext
  {
    public DbSet<JobProcess> JobProcesses { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<JobProcess>();
    }
  }
}

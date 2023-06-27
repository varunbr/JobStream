using JobStream.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobStream.Data
{
  public class DataContext : DbContext
  {
    public DbSet<JobProcess> JobProcesses { get; set; }
    public DbSet<JobProcessHistory> JobStreamHistories { get; set; }
    public DbSet<JobBlock> JobBlocks { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<JobResult> JobResults { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<JobProcess>()
        .HasMany(jp => jp.Histories)
        .WithOne(jh => jh.JobProcess)
        .HasForeignKey(jh => jh.JobProcessId);
      modelBuilder.Entity<JobProcess>()
        .HasMany(jp => jp.JobBlocks)
        .WithOne(jb => jb.JobProcess)
        .HasForeignKey(jb => jb.JobProcessId);

      modelBuilder.Entity<JobBlock>()
        .HasOne(jb => jb.ConditionBlock)
        .WithOne(jb => jb.ParentConditionBlock)
        .HasForeignKey<JobBlock>(jb => jb.ConditionBlockId);
      modelBuilder.Entity<JobBlock>()
        .HasOne(jb => jb.IfBlock)
        .WithOne(jb => jb.ParentIfBlock)
        .HasForeignKey<JobBlock>(jb => jb.IfBlockId);
      modelBuilder.Entity<JobBlock>()
        .HasOne(jb => jb.ElseBlock)
        .WithOne(jb => jb.ParentElseBlock)
        .HasForeignKey<JobBlock>(jb => jb.ElseBlockId)
        .IsRequired(false);
      modelBuilder.Entity<JobBlock>()
        .Property(jb => jb.JobBlockType)
        .HasConversion<string>();
      modelBuilder.Entity<JobBlock>()
        .Property(jb => jb.ExecutionType)
        .HasConversion<string>();
      modelBuilder.Entity<JobBlock>()
        .Property(jb => jb.ExecutionResultType)
        .HasConversion<string>();

      modelBuilder.Entity<Job>()
        .HasOne(j => j.JobProcess)
        .WithMany(jp => jp.Jobs)
        .HasForeignKey(j => j.JobProcessId);
      modelBuilder.Entity<Job>()
        .HasOne(j => j.JobBlock)
        .WithMany(jb => jb.Jobs)
        .HasForeignKey(j => j.JobBlockId);

      modelBuilder.Entity<JobResult>()
        .HasOne(jr => jr.JobProcess)
        .WithMany(jr => jr.JobResults)
        .HasForeignKey(jr => jr.JobProcessId);
      modelBuilder.Entity<JobResult>()
        .HasOne(jr => jr.Job)
        .WithMany(jr => jr.JobResults)
        .HasForeignKey(jr => jr.JobId);
      modelBuilder.Entity<JobResult>()
        .HasOne(jr => jr.JobProcessHistory)
        .WithMany(jr => jr.JobResults)
        .HasForeignKey(jr => jr.RunId);
      modelBuilder.Entity<JobResult>()
        .Property(jp => jp.RunStatus)
        .HasConversion<string>();

      modelBuilder.Entity<JobProcessHistory>()
        .Property(jh => jh.Status)
        .HasConversion<string>();
    }
  }
}

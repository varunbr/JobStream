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
      await SeedJobProcesses();
      await SeedJobs();
    }

    private async Task SeedJobProcesses()
    {
      if (await _context.JobProcesses.AnyAsync())
        return;
      _context.JobProcesses.Add(new JobProcess
      {
        Name = "First job stream",
        Updated = DateTime.UtcNow
      });
      await _context.SaveChangesAsync();
    }

    private async Task SeedJobs()
    {
      if (await _context.JobBlocks.AnyAsync())
        return;
      var jobProcess = await _context.JobProcesses.FirstAsync();
      _context.JobBlocks.Add(new JobBlock
      {
        JobBlockType = JobBlockType.Conditional,
        Depth = 1,
        JobProcess = jobProcess,
        ConditionBlock = new JobBlock
        {
          JobBlockType = JobBlockType.Collection,
          Depth = 2,
          JobProcess = jobProcess,
          Jobs = new List<Job>
          {
            new()
            {
              Name = "Condition Job 1",
              JobProcess = jobProcess,
              Order = 1
            },
            new ()
            {
              Name = "Condition Job 2",
              JobProcess = jobProcess,
              Order = 2
            }
          },
          ExecutionResultType = ExecutionResultType.All,
          ExecutionType = ExecutionType.Sequential
        },
        IfBlock = new JobBlock
        {
          JobBlockType = JobBlockType.Collection,
          Depth = 2,
          JobProcess = jobProcess,
          Jobs = new List<Job>
          {
            new()
            {
              Name = "Condition success Job 1",
              JobProcess = jobProcess,
              Order = 1
            },
            new ()
            {
              Name = "Condition success Job 2",
              JobProcess = jobProcess,
              Order = 2
            }
          },
          ExecutionResultType = ExecutionResultType.Any,
          ExecutionType = ExecutionType.Sequential
        },
        ElseBlock = new JobBlock
        {
          JobBlockType = JobBlockType.Collection,
          Depth = 2,
          JobProcess = jobProcess,
          Jobs = new List<Job>
          {
            new()
            {
              Name = "Condition fail Job 1",
              JobProcess = jobProcess,
              Order = 1
            },
            new ()
            {
              Name = "Condition fail Job 2",
              JobProcess = jobProcess,
              Order = 2
            }
          },
          ExecutionResultType = ExecutionResultType.All,
          ExecutionType = ExecutionType.Parallel
        }

      });
      await _context.SaveChangesAsync();
    }
  }
}

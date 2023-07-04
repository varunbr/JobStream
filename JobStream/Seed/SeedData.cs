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
        Name = "Job stream With conditions",
        Updated = DateTime.UtcNow
      });
      await _context.SaveChangesAsync();
      _context.JobProcesses.Add(new JobProcess
      {
        Name = "Simple Job stream with Collection",
        Updated = DateTime.UtcNow
      });
      await _context.SaveChangesAsync();
    }

    private async Task SeedJobs()
    {
      if (await _context.JobBlocks.AnyAsync())
        return;
      var jobProcess = await _context.JobProcesses.FirstAsync(jp => jp.Id == 1);
      _context.JobBlocks.Add(new JobBlock
      {
        BlockType = JobBlockType.Conditional,
        Depth = 1,
        JobProcess = jobProcess,
        ConditionBlock = new JobBlock
        {
          BlockType = JobBlockType.Collection,
          Depth = 2,
          JobProcess = jobProcess,
          Jobs = new List<Job>
          {
            new()
            {
              Name = "Condition Job 1",
              JobProcess = jobProcess,
              Order = 1,
              MockResultStatus = "Success",
              MockResult = true,
              MockDuration = 1000
            },
            new ()
            {
              Name = "Condition Job 2",
              JobProcess = jobProcess,
              Order = 2,
              MockResultStatus = "Canceled",
              MockResult = false,
              MockDuration = 1000
            }
          },
          ExecutionResultType = ExecutionResultType.All,
          ExecutionType = ExecutionType.Sequential
        },
        IfBlock = new JobBlock
        {
          BlockType = JobBlockType.Collection,
          Depth = 2,
          JobProcess = jobProcess,
          Jobs = new List<Job>
          {
            new()
            {
              Name = "Condition success Job 1",
              JobProcess = jobProcess,
              Order = 1,
              MockResultStatus = "Done",
              MockResult = true,
              MockDuration = 1000
            },
            new ()
            {
              Name = "Condition success Job 2",
              JobProcess = jobProcess,
              Order = 2,
              MockResultStatus = "NotDone",
              MockResult = false,
              MockDuration = 1000
            }
          },
          ExecutionResultType = ExecutionResultType.Any,
          ExecutionType = ExecutionType.Sequential
        },
        ElseBlock = new JobBlock
        {
          BlockType = JobBlockType.Collection,
          Depth = 2,
          JobProcess = jobProcess,
          Jobs = new List<Job>
          {
            new()
            {
              Name = "Condition fail Job 1",
              JobProcess = jobProcess,
              Order = 1,
              MockResultStatus = "Done",
              MockResult = true,
              MockDuration = 2000
            },
            new ()
            {
              Name = "Condition fail Job 2",
              JobProcess = jobProcess,
              Order = 2,
              MockResultStatus = "Failed",
              MockResult = false,
              MockDuration = 1000
            },
            new ()
            {
              Name = "Condition fail Job 3",
              JobProcess = jobProcess,
              Order = 3,
              MockResultStatus = "Failed",
              MockResult = false,
              MockDuration = 1000
            },
            new ()
            {
              Name = "Condition fail Job 4",
              JobProcess = jobProcess,
              Order = 4,
              MockResultStatus = "Failed",
              MockResult = false,
              MockDuration = 1000
            }
          },
          ExecutionResultType = ExecutionResultType.Any,
          ExecutionType = ExecutionType.Parallel
        }
      });
      jobProcess = await _context.JobProcesses.FirstAsync(jp => jp.Id == 2);
      _context.JobBlocks.Add(new JobBlock
      {
        BlockType = JobBlockType.Collection,
        Depth = 1,
        JobProcess = jobProcess,
        Jobs = new List<Job>
        {
          new()
          {
            Name = "Job 1",
            JobProcess = jobProcess,
            Order = 1,
            MockResultStatus = "Success",
            MockResult = true,
            MockDuration = 1000
          },
          new ()
          {
            Name = "Job 2",
            JobProcess = jobProcess,
            Order = 2,
            MockResultStatus = "Failed",
            MockResult = false,
            MockDuration = 1000
          },
          new ()
          {
            Name = "Job 3",
            JobProcess = jobProcess,
            Order = 3,
            MockResultStatus = "Failed",
            MockResult = false,
            MockDuration = 1000
          }
        },
        ExecutionResultType = ExecutionResultType.All,
        ExecutionType = ExecutionType.Parallel
      });
      await _context.SaveChangesAsync();
    }
  }
}

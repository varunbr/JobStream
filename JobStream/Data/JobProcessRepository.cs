using JobStream.Entities;
using JobStream.Helpers;
using Microsoft.EntityFrameworkCore;

namespace JobStream.Data
{
  public class JobProcessRepository
  {
    private readonly DataContext _dataContext;

    public JobProcessRepository(DataContext dataContext)
    {
      _dataContext = dataContext;
    }

    public async Task<JobProcess?> GetJobProcess(int jobProcessId)
    {
      return await _dataContext.JobProcesses
        .FirstOrDefaultAsync(jp => jp.Id == jobProcessId);
    }

    public async Task<List<JobProcess>> GetJobProcess()
    {
      return await _dataContext.JobProcesses
        .AsNoTracking()
        .ToListAsync();
    }

    public async Task<List<JobBlock>> GetJobBlocks(int jobProcessId)
    {
      return await _dataContext.JobBlocks
        .Where(jb => jb.JobProcessId == jobProcessId)
        .Include(jb => jb.Jobs)
        .AsNoTracking()
        .ToListAsync();
    }

    public async Task<JobProcess> CreateJobProcess(JobProcess jobProcess, JobBlock jobBlock)
    {
      _dataContext.JobProcesses.Add(jobProcess);
      _dataContext.JobBlocks.Add(jobBlock);

      if (await _dataContext.SaveChangesAsync() > 0)
        return jobProcess;
      throw new HttpException("Failed to update JobProcess.", 500);
    }
  }
}

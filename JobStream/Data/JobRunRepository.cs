using JobStream.Entities;
using JobStream.Helpers;
using Microsoft.EntityFrameworkCore;

namespace JobStream.Data
{
  public class JobRunRepository
  {
    private readonly DataContext _context;
    public JobRunRepository(DataContext context)
    {
      _context = context;
    }

    public async Task<bool> IsJobProcessInQueue(int jobProcessId)
    {
      return await _context.JobStreamHistories
        .AnyAsync(jh => jh.JobProcessId == jobProcessId && jh.Status == JobProcessStatus.InQueue);
    }

    public async Task<JobProcessHistory> AddToQueue(JobProcessHistory processHistory)
    {
      _context.JobStreamHistories.Add(processHistory);
      if (await _context.SaveChangesAsync() > 0)
        return processHistory;
      throw new HttpException("Failed to save JobProcess in queue");
    }

    public async Task<JobProcessHistory?> GetNextItemFromQueue()
    {
      return await _context.JobStreamHistories
        .Where(j => j.Status == JobProcessStatus.InQueue)
        .OrderBy(j => j.Added)
        .FirstOrDefaultAsync();
    }

    public async Task<JobProcessHistory> GetProcessHistory(int runId)
    {
      return await _context.JobStreamHistories.FirstAsync(j => j.Id == runId);
    }

    public async Task<JobProcessHistory> Update(JobProcessHistory processHistory)
    {
      _context.JobStreamHistories.Update(processHistory);
      if (await _context.SaveChangesAsync() > 0)
        return processHistory;
      throw new HttpException("Failed to update JobProcessHistory");
    }

    public async Task<List<JobBlock>> GetJobBlocks(int jobProcessId)
    {
      return await _context.JobBlocks
        .Include(j => j.Jobs)
        .Where(jb => jb.JobProcessId == jobProcessId)
        .ToListAsync();
    }

    public JobResult AddJobResult(JobResult jobResult)
    {
      lock (_context)
      {
        _context.JobResults.Add(jobResult);
        var result = _context.SaveChanges();
        _context.Entry(jobResult).State = EntityState.Detached;
        if (result > 0)
          return jobResult;
        throw new HttpException("Failed to add JobResult");
      }
    }

    public JobResult UpdateJobResult(JobResult jobResult)
    {
      lock (_context)
      {
        _context.JobResults.Update(jobResult);
        var result = _context.SaveChanges();
        _context.Entry(jobResult).State = EntityState.Detached;
        if (result > 0)
          return jobResult;
        throw new HttpException("Failed to update JobResult");
      }
    }
  }
}

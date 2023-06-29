using AutoMapper;
using JobStream.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobStream.Data
{
  public class JobHistoryRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public JobHistoryRepository(DataContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<List<JobProcessHistory>> GetAll()
    {
      return await _context.JobStreamHistories
        .AsNoTracking()
        .ToListAsync();
    }

    public async Task<JobProcessHistory?> Get(int runId)
    {
      return await _context.JobStreamHistories
        .Include(jh => jh.JobResults)
        .FirstOrDefaultAsync(jh => jh.Id == runId);
    }
  }
}

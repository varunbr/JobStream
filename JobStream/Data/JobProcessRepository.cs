using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobStream.DTOs;
using JobStream.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobStream.Data
{
  public class JobProcessRepository
  {
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public JobProcessRepository(DataContext dataContext, IMapper mapper)
    {
      _dataContext = dataContext;
      _mapper = mapper;
    }

    public async Task<JobProcess?> GetJobProcess(int jobProcessId)
    {
      return await _dataContext.JobProcesses
        .FirstOrDefaultAsync(jp => jp.Id == jobProcessId);
    }
  }
}

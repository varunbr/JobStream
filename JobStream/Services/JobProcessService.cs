using AutoMapper;
using JobStream.Data;
using JobStream.DTOs;
using JobStream.Entities;
using JobStream.Helpers;

namespace JobStream.Services
{
  public class JobProcessService
  {
    private readonly DataContext _context;
    private readonly JobProcessRepository _jobProcessRepository;
    private readonly JobRunRepository _historyRepository;
    private readonly IMapper _mapper;

    public JobProcessService(DataContext context,
      JobProcessRepository jobProcessRepository,
      JobRunRepository historyRepository,
      IMapper mapper)
    {
      _context = context;
      _jobProcessRepository = jobProcessRepository;
      _historyRepository = historyRepository;
      _mapper = mapper;
    }

    public async Task<JobProcessHistoryDto> InvokeJobStream(int jobProcessId)
    {
      var jobProcess = await _jobProcessRepository.GetJobProcess(jobProcessId);

      if (jobProcess == null)
        throw new HttpException($"JobProcess doesn't exist with Id:{jobProcessId}");

      if (await _historyRepository.IsJobProcessInQueue(jobProcessId))
        throw new HttpException("JobProcess already in the queue.");

      var jobHistory = new JobProcessHistory
      {
        JobProcess = jobProcess,
        Status = JobProcessStatus.InQueue,
        Added = DateTime.UtcNow,
        JobProcessName = jobProcess.Name
      };

      jobHistory = await _historyRepository.AddToQueue(jobHistory);

      return _mapper.Map<JobProcessHistoryDto>(jobHistory);
    }
  }
}

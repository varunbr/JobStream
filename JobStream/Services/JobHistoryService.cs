using AutoMapper;
using JobStream.Data;
using JobStream.DTOs;
using JobStream.Entities;
using JobStream.Helpers;

namespace JobStream.Services
{
  public class JobHistoryService
  {
    private readonly JobHistoryRepository _historyRepository;
    private readonly IMapper _mapper;

    public JobHistoryService(JobHistoryRepository historyRepository, IMapper mapper)
    {
      _historyRepository = historyRepository;
      _mapper = mapper;
    }

    public async Task<List<JobProcessHistoryDto>> GetAll()
    {
      var jobHistories = await _historyRepository.GetAll();
      return jobHistories.Select(_mapper.Map<JobProcessHistoryDto>).ToList();
    }

    public async Task<JobProcessHistoryDto> Get(int runId)
    {
      var jobHistory = await _historyRepository.Get(runId);
      if (jobHistory == null)
        throw new HttpException($"JobProcessHistory not found with id {runId}");
      return _mapper.Map<JobProcessHistoryDto>(jobHistory);
    }
  }
}

using JobStream.DTOs;
using JobStream.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobStream.Controllers
{
  public class JobProcessController : BaseController
  {
    private readonly JobProcessService _jobProcessService;
    private readonly JobHistoryService _historyService;

    public JobProcessController(JobProcessService jobProcessService, JobHistoryService historyService)
    {
      _jobProcessService = jobProcessService;
      _historyService = historyService;
    }

    [HttpPost("addToQueue/{jobProcessId}")]
    public async Task<JobProcessHistoryDto> AddToQueue(int jobProcessId)
    {
      return await _jobProcessService.AddToQueue(jobProcessId);
    }

    [HttpPost("create")]
    public async Task<JobProcessDto> Create(JobProcessDto jobProcessDto)
    {
      return await _jobProcessService.CreateJobProcess(jobProcessDto);
    }

    [HttpGet("{jobProcessId}")]
    public async Task<JobProcessDto> GetJobProcess(int jobProcessId)
    {
      return await _jobProcessService.GetDetailedJobProcess(jobProcessId);
    }

    [HttpGet]
    public async Task<List<JobProcessDto>> GetJobProcess()
    {
      return await _jobProcessService.GetJobProcess();
    }

    [HttpGet("history")]
    public async Task<List<JobProcessHistoryDto>> GetJobProcessHistories()
    {
      return await _historyService.GetAll();
    }

    [HttpGet("history/{runId}")]
    public async Task<JobProcessHistoryDto> GetJobProcessHistory(int runId)
    {
      return await _historyService.Get(runId);
    }
  }
}

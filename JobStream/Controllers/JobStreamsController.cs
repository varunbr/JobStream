using JobStream.DTOs;
using JobStream.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobStream.Controllers
{
  public class JobProcessController : BaseController
  {
    private readonly JobProcessService _jobProcessService;
    public JobProcessController(JobProcessService jobProcessService)
    {
      _jobProcessService = jobProcessService;
    }

    [HttpPost("addToQueue/{jobProcessId}")]
    public async Task<JobProcessHistoryDto> AddToQueue(int jobProcessId)
    {
      return await _jobProcessService.InvokeJobStream(jobProcessId);
    }
  }
}

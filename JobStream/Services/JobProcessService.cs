using AutoMapper;
using JobStream.Data;
using JobStream.DTOs;
using JobStream.Entities;
using JobStream.Helpers;

namespace JobStream.Services
{
  public class JobProcessService
  {
    private readonly JobProcessRepository _jobProcessRepository;
    private readonly JobRunRepository _historyRepository;
    private readonly IMapper _mapper;

    public JobProcessService(
      JobProcessRepository jobProcessRepository,
      JobRunRepository historyRepository,
      IMapper mapper)
    {
      _jobProcessRepository = jobProcessRepository;
      _historyRepository = historyRepository;
      _mapper = mapper;
    }

    public async Task<JobProcessHistoryDto> AddToQueue(int jobProcessId)
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

    public async Task<List<JobProcessDto>> GetJobProcess()
    {
      var jobProcess = await _jobProcessRepository.GetJobProcess();
      return jobProcess.Select(_mapper.Map<JobProcessDto>).ToList();
    }

    public async Task<JobProcessDto> GetDetailedJobProcess(int jobProcessId)
    {
      var jobProcess = await _jobProcessRepository.GetJobProcess(jobProcessId);
      if (jobProcess == null)
        throw new HttpException($"JobProcess not found with id {jobProcessId}");
      var jobBlocks = await _jobProcessRepository.GetJobBlocks(jobProcessId);
      return MapToJobProcessDto(jobProcess, jobBlocks);
    }

    private JobProcessDto MapToJobProcessDto(JobProcess jobProcess, List<JobBlock> jobBlocks)
    {
      var jobProcessDto = _mapper.Map<JobProcessDto>(jobProcess);
      jobProcessDto.JobBlock = MapToJobBlockDto(jobBlocks, jobBlocks.First(jb => jb.Depth == 1).Id);
      return jobProcessDto;
    }

    private JobBlockDto? MapToJobBlockDto(List<JobBlock> jobBlocks, int? blockId)
    {
      if (blockId == null)
        return null;
      var jobBlockDto = _mapper.Map<JobBlockDto>(jobBlocks.First(jb => jb.Id == blockId));
      jobBlockDto.ConditionBlock = MapToJobBlockDto(jobBlocks, jobBlockDto.ConditionBlockId);
      jobBlockDto.IfBlock = MapToJobBlockDto(jobBlocks, jobBlockDto.IfBlockId);
      jobBlockDto.ElseBlock = MapToJobBlockDto(jobBlocks, jobBlockDto.ElseBlockId);
      return jobBlockDto;
    }

    public async Task<JobProcessDto> CreateJobProcess(JobProcessDto jobProcessDto)
    {
      var jobProcess = ValidateAndMapToJobProcess(jobProcessDto);
      var jobBlock = ValidateAndMapToJobBlock(jobProcess, jobProcessDto.JobBlock, 1)!;
      await _jobProcessRepository.CreateJobProcess(jobProcess, jobBlock);
      return await GetDetailedJobProcess(jobProcess.Id);
    }

    private JobProcess ValidateAndMapToJobProcess(JobProcessDto jobProcessDto)
    {
      if (string.IsNullOrWhiteSpace(jobProcessDto.Name))
        throw new HttpException("JobProcess name must not be empty.");
      var jobProcess = _mapper.Map<JobProcess>(jobProcessDto);
      jobProcess.Updated = DateTime.UtcNow;
      return jobProcess;
    }

    private JobBlock? ValidateAndMapToJobBlock(JobProcess jobProcess, JobBlockDto? blockDto, int depth)
    {
      if (blockDto == null)
        return null;

      var block = _mapper.Map<JobBlock>(blockDto);
      block.Depth = depth;
      block.JobProcess = jobProcess;

      if (blockDto.BlockType == JobBlockType.Collection)
      {
        if (blockDto.ConditionBlock != null || blockDto.IfBlock != null || blockDto.ElseBlock != null)
          throw new HttpException("Collection BlockType should not have any child blocks.");
        block.Jobs = ValidateAndMapToJobs(jobProcess, blockDto.Jobs);
      }
      else if (blockDto.BlockType == JobBlockType.Conditional)
      {
        if (blockDto.ExecutionResultType != null || blockDto.ExecutionResultType != null)
          throw new HttpException("Conditional BlockType should not have any Collection block properties.");
        if (blockDto.ConditionBlock == null)
          throw new HttpException("ConditionBlock is mandatory for BlockType of Conditional.");
        block.ConditionBlock = ValidateAndMapToJobBlock(jobProcess, blockDto.ConditionBlock, depth + 1)!;
        block.IfBlock = ValidateAndMapToJobBlock(jobProcess, blockDto.ConditionBlock, depth + 1);
        block.ElseBlock = ValidateAndMapToJobBlock(jobProcess, blockDto.ConditionBlock, depth + 1);
      }
      else
      {
        throw new HttpException("Invalid BlockType");
      }
      return block;
    }

    private List<Job> ValidateAndMapToJobs(JobProcess jobProcess, List<JobDto> jobDtoList)
    {
      if (jobDtoList.Count == 0)
        throw new HttpException("Collection BlockType should have at least one Job.");
      var jobList = new List<Job>();
      foreach (var jobDto in jobDtoList)
      {
        if (string.IsNullOrWhiteSpace(jobDto.Name))
          throw new HttpException("Job name must not be empty.");
        var job = _mapper.Map<Job>(jobDto);
        job.JobProcess = jobProcess;
        jobList.Add(job);
      }
      return jobList;
    }
  }
}

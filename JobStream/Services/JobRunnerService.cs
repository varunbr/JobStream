using JobStream.Data;
using JobStream.Entities;
using JobStream.Helpers;

namespace JobStream.Services
{
  public class JobRunnerService
  {
    private readonly JobRunRepository _historyRepository;
    private readonly ILogger<JobRunnerService> _logger;

    public JobRunnerService(JobRunRepository historyRepository, ILogger<JobRunnerService> logger)
    {
      _historyRepository = historyRepository;
      _logger = logger;
    }

    public async Task InvokeJobProcess(int runId)
    {
      var runItem = await _historyRepository.GetProcessHistory(runId);
      try
      {
        runItem.Status = JobProcessStatus.Running;
        runItem.Started = DateTime.UtcNow;
        await _historyRepository.Update(runItem);

        if (await StartJobProcess(runItem))
          runItem.Status = JobProcessStatus.Success;
        else
          runItem.Status = JobProcessStatus.Failed;
      }
      catch (OperationCanceledException ex)
      {
        runItem.Status = JobProcessStatus.Canceled;
        runItem.Comment = ex.Message;
      }
      catch (Exception ex)
      {
        runItem.Status = JobProcessStatus.Failed;
        runItem.Comment = ex.Message;
      }
      runItem.Finished = DateTime.UtcNow;
      await _historyRepository.Update(runItem);
    }

    private async Task<bool> StartJobProcess(JobProcessHistory processHistory)
    {
      var blocks = await _historyRepository.GetJobBlocks(processHistory.JobProcessId);

      var parentBlock = blocks.First(b => b.Depth == 1);

      return await ProcessBlock(new JobRunContext(blocks, processHistory, 1), parentBlock);
    }

    private async Task<bool> ProcessBlock(JobRunContext runContext, JobBlock currentBlock)
    {
      return currentBlock.BlockType switch
      {
        JobBlockType.Collection => await ProcessCollectionBlock(runContext, currentBlock),
        JobBlockType.Conditional => await ProcessConditionBlock(runContext, currentBlock),
        _ => true
      };
    }

    private async Task<bool> ProcessConditionBlock(JobRunContext runContext, JobBlock currentBlock)
    {
      if (currentBlock.ConditionBlockId == null)
        return false;
      var conditionBlock = runContext.JobBlocks.First(j => j.Id == currentBlock.ConditionBlockId);
      if (await ProcessBlock(runContext, conditionBlock))
      {
        if (currentBlock.IfBlockId == null)
          return true;
        var ifBlock = runContext.JobBlocks.First(j => j.Id == currentBlock.IfBlockId);
        return await ProcessBlock(runContext, ifBlock);
      }
      else
      {
        if (currentBlock.ElseBlockId == null)
          return false;
        var elseBlock = runContext.JobBlocks.First(j => j.Id == currentBlock.ElseBlockId);
        return await ProcessBlock(runContext, elseBlock);
      }
    }

    private async Task<bool> ProcessCollectionBlock(JobRunContext runContext, JobBlock currentBlock)
    {
      List<bool> result;
      if (currentBlock.ExecutionType == ExecutionType.Sequential)
      {
        result = await RunJobSequentially(runContext, currentBlock);
      }
      else if (currentBlock.ExecutionType == ExecutionType.Parallel)
      {
        result = await RunJobParallel(runContext, currentBlock);
      }
      else
      {
        return false;
      }

      if (currentBlock.ExecutionResultType == ExecutionResultType.Any)
      {
        return result.Any(r => r);
      }
      else
      {
        return result.All(r => r);
      }
    }

    private async Task<List<bool>> RunJobSequentially(JobRunContext runContext, JobBlock currentBlock)
    {
      var resultList = new List<bool>();
      foreach (var job in currentBlock.Jobs.OrderBy(j => j.Order))
      {
        var result = await StartJob(runContext, job, runContext.CurrentStep++);
        resultList.Add(result);
      }
      return resultList;
    }

    private async Task<List<bool>> RunJobParallel(JobRunContext runContext, JobBlock currentBlock)
    {
      var tasks = new List<Task<bool>>();
      foreach (var job in currentBlock.Jobs.OrderBy(j => j.Order))
      {
        tasks.Add(StartJob(runContext, job, runContext.CurrentStep));
      }
      await Task.WhenAll(tasks);
      return tasks.Select(t => t.Result).ToList();
    }

    private async Task<bool> StartJob(JobRunContext runContext, Job job, int step)
    {
      _logger.LogWarning($"Starting Job: {job.Name}({job.Id})");
      var jobResult = new JobResult
      {
        Job = job,
        JobName = job.Name,
        Order = step,
        RunStatus = JobRunStatus.Running,
        Started = DateTime.UtcNow,
        JobProcessId = runContext.JobProcessHistory.JobProcessId,
        RunId = runContext.JobProcessHistory.Id
      };
      jobResult = _historyRepository.AddJobResult(jobResult);

      //-------------- MOCKING JOB RUN ---------------------
      await Task.Delay(TimeSpan.FromMilliseconds(job.MockDuration));
      jobResult.IsSuccess = job.MockResult;
      jobResult.RunStatus = job.MockResult ? JobRunStatus.Success : JobRunStatus.Failed;
      jobResult.ResultStatus = job.MockResultStatus;
      //--------------------END-----------------------------

      jobResult.Ended = DateTime.UtcNow;
      _historyRepository.UpdateJobResult(jobResult);
      _logger.LogWarning($"Completed Job: {job.Name}({job.Id})");
      return job.MockResult;
    }
  }
}

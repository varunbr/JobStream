using JobStream.Entities;

namespace JobStream.Helpers
{
  public class JobRunContext
  {
    public List<JobBlock> JobBlocks { get; }
    public JobProcessHistory JobProcessHistory { get; }
    public int CurrentStep { get; set; }

    public JobRunContext(List<JobBlock> jobBlocks, JobProcessHistory jobProcessHistory, int currentStep)
    {
      JobBlocks = jobBlocks;
      JobProcessHistory = jobProcessHistory;
      CurrentStep = currentStep;
    }
  }
}

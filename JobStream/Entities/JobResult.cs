#pragma warning disable CS8618
namespace JobStream.Entities
{
  public class JobResult
  {
    public int Id { get; set; }
    public int JobProcessId { get; set; }
    public JobProcess JobProcess { get; set; }
    public int RunId { get; set; }
    public JobProcessHistory JobProcessHistory { get; set; }
    public int JobId { get; set; }
    public Job Job { get; set; }
    public int Order { get; set; }
    public JobRunStatus RunStatus { get; set; }
    public string? ResultStatus { get; set; }
    public bool? IsSuccess { get; set; }
    public DateTime Started { get; set; }
    public DateTime? Ended { get; set; }
  }
}

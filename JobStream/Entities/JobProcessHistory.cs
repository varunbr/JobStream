#pragma warning disable CS8618
namespace JobStream.Entities
{
  public class JobProcessHistory
  {
    public int Id { get; set; }
    public int JobProcessId { get; set; }
    public JobProcess JobProcess { get; set; }
    public DateTime Added { get; set; }
    public DateTime? Started { get; set; }
    public DateTime? Finished { get; set; }
    public JobProcessStatus Status { get; set; }
    public List<JobResult> JobResults { get; set; }
  }
}

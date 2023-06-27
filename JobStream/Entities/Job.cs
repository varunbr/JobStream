#pragma warning disable CS8618
namespace JobStream.Entities
{
  public class Job
  {
    public int Id { get; set; }
    public int Order { get; set; }
    public string Name { get; set; }
    public int JobProcessId { get; set; }
    public JobProcess JobProcess { get; set; }
    public int JobBlockId { get; set; }
    public JobBlock JobBlock { get; set;}
    public List<JobResult> JobResults { get; set; }
  }
}

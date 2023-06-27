﻿#pragma warning disable CS8618
namespace JobStream.Entities
{
  public class JobProcess
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Updated { get; set; }
    public int JobBlockId { get; set; }
    public JobBlock JobBlock { get; set; }
    public List<JobProcessHistory> Histories { get; set; }
    public List<Job> Jobs { get; set; }
    public List<JobResult> JobResults { get; set; }
  }
}

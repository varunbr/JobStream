using JobStream.Entities;

namespace JobStream.DTOs
{
  public class JobProcessHistoryDto
  {
    public int Id { get; set; }
    public int JobProcessId { get; set; }
    public string JobProcessName { get; set; }
    public DateTime Added { get; set; }
    public DateTime? Started { get; set; }
    public DateTime? Finished { get; set; }
    public JobProcessStatus Status { get; set; }
    public string? Comment { get; set; }
    public List<JobResultDto>? JobResults { get; set; }
  }
}

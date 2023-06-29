using JobStream.Entities;

#nullable disable
namespace JobStream.DTOs
{
  public class JobResultDto
  {
    public int Id { get; set; }
    public string JobName { get; set; }
    public int JobProcessId { get; set; }
    public int RunId { get; set; }
    public int JobId { get; set; }
    public int Order { get; set; }
    public JobRunStatus RunStatus { get; set; }
    public string ResultStatus { get; set; }
    public bool? IsSuccess { get; set; }
    public DateTime Started { get; set; }
    public DateTime? Ended { get; set; }
  }
}

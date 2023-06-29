#nullable disable
namespace JobStream.DTOs
{
  public class JobDto
  {
    public int Id { get; set; }
    public int Order { get; set; }
    public string Name { get; set; }
    public int JobProcessId { get; set; }
    public int JobBlockId { get; set; }
    public string MockResultStatus { get; set; }
    public bool MockResult { get; set; }
    public int MockDuration { get; set; }
  }
}

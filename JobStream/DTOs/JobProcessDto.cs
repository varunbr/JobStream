#nullable disable

namespace JobStream.DTOs
{
  public class JobProcessDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Updated { get; set; }
    public JobBlockDto JobBlock { get; set; }
  }
}

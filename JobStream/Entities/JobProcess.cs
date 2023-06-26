namespace JobStream.Entities
{
  public class JobProcess
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Updated { get; set; }
  }
}

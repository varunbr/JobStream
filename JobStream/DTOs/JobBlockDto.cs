using JobStream.Entities;

namespace JobStream.DTOs
{
  public class JobBlockDto
  {
    public int Id { get; set; }
    public int Depth { get; set; }
    public int JobProcessId { get; set; }
    public int? IfBlockId { get; set; }
    public JobBlockDto? IfBlock { get; set; }
    public int? ConditionBlockId { get; set; }
    public JobBlockDto? ConditionBlock { get; set; }
    public int? ElseBlockId { get; set; }
    public JobBlockDto? ElseBlock { get; set; }
    public JobBlockType BlockType { get; set; }
    public List<JobDto> Jobs { get; set; } = new();
    public ExecutionType? ExecutionType { get; set; }
    public ExecutionResultType? ExecutionResultType { get; set; }
  }
}

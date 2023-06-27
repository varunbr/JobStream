#pragma warning disable CS8618
namespace JobStream.Entities
{
  public class JobBlock
  {
    public int Id { get; set; }
    public int? IfBlockId { get; set; }
    public JobBlock IfBlock { get; set; }
    public int? ConditionBlockId { get; set; }
    public JobBlock ConditionBlock { get; set; }
    public int? ElseBlockId { get; set; }
    public JobBlock ElseBlock { get; set; }
    public JobBlockType JobBlockType { get; set; }
    public List<Job> Jobs { get; set; } = new();
    public ExecutionType? ExecutionType { get; set; }
    public ExecutionResultType? ExecutionResultType { get; set; }
    public JobProcess JobProcess { get; set; }
    public JobBlock ParentConditionBlock { get; set; }
    public JobBlock ParentIfBlock { get; set; }
    public JobBlock ParentElseBlock { get; set; }
  }
}

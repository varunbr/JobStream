namespace JobStream.Entities
{
  public enum ExecutionResultType
  {
    None,
    All,
    Any
  }

  public enum ExecutionType
  {
    None,
    Sequential,
    Parallel
  }

  public enum JobBlockType
  {
    None,
    Collection,
    Conditional
  }

  public enum JobProcessStatus
  {
    None,
    InQueue,
    Running,
    Success,
    Failed,
    Canceled
  }

  public enum JobRunStatus
  {
    None,
    Started,
    Running,
    Success,
    Failed
  }
}

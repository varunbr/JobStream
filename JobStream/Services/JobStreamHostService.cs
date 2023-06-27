namespace JobStream.Services
{
  public class JobStreamHostService : BackgroundService
  {
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<JobStreamHostService> _logger;

    public JobStreamHostService(IServiceProvider serviceProvider, ILogger<JobStreamHostService> logger)
    {
      _serviceProvider = serviceProvider;
      _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {

        await Task.Delay(1000, stoppingToken);
      }
    }
  }
}

using JobStream.Data;

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
        try
        {
          using var scope = _serviceProvider.CreateScope();
          var jobRunProcess = await scope.ServiceProvider.GetRequiredService<JobRunRepository>()
            .GetNextItemFromQueue();
          if (jobRunProcess != null)
          {
            var jobRunnerService = scope.ServiceProvider.GetRequiredService<JobRunnerService>();
            await jobRunnerService.InvokeJobProcess(jobRunProcess.Id);
          }
        }
        catch (Exception ex)
        {
          _logger.LogError(ex.Message, ex);
        }
        await Task.Delay(5000, stoppingToken);
      }
    }
  }
}

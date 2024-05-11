using Coravel.Invocable;

namespace CoravelScheduledWorker;

public class WorkerInvocable : IInvocable, ICancellableInvocable
{
    private readonly ILogger<Worker> _logger;

    public CancellationToken CancellationToken { get; set; }

    public WorkerInvocable(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    public async Task Invoke()
    {
        if (!CancellationToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker started at: {time}", DateTimeOffset.Now);
            }

            await Task.Delay(1000, CancellationToken);

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker completed at: {time}", DateTimeOffset.Now);
            }
        }

        //throw new InvalidOperationException("Test error handling");
    }
}

using TimeStone;

namespace ConsoleSample;

public class ConsumerWorker : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<ConsumerWorker> _logger;

    public ConsumerWorker(IServiceScopeFactory serviceScopeFactory, ILogger<ConsumerWorker> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var runner = scope.ServiceProvider.GetRequiredService<IRecurrenceRunner>();

        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await Task.Delay(10_000, stoppingToken);

            await runner.ProcessAsync();
        }
    }
}

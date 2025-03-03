using Cronos;

using TimeStone;

namespace ConsoleSample;

public class ProducerWorker : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<ProducerWorker> _logger;

    public ProducerWorker(IServiceScopeFactory serviceScopeFactory, ILogger<ProducerWorker> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var manager = scope.ServiceProvider.GetRequiredService<IRecurrenceManager>();

        var greetingAlice = new RecurringTask
        {
            Name = "Greeting",
            Target = "Alice",
            Cron = CronExpression.Parse("* * * * *"),
            StartDate = DateTime.UtcNow,
            FinishDate = DateTime.UtcNow.AddMinutes(5),
            Status = RecurringTaskStatus.Ready,
        };

        var greetingBob = new RecurringTask
        {
            Name = "Greeting",
            Target = "Bob",
            Cron = CronExpression.Parse("* * * * *"),
            StartDate = DateTime.UtcNow,
            FinishDate = DateTime.UtcNow.AddMinutes(5),
            Status = RecurringTaskStatus.Ready,
        };

        await manager.CreateAsync(greetingAlice, stoppingToken);
    }
}

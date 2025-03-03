using TimeStone;

namespace ConsoleSample.Migrations.Handlers;

[RecurrenceHandler("Greeting")]
public class GreetingHandler : IRecurrenceHandler
{
    private readonly ILogger<GreetingHandler> _logger;

    public GreetingHandler(ILogger<GreetingHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(IRecurrenceContext context, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Execution time: {executionTime}", context.ExecutionTime);

        _logger.LogInformation("[{currentExecution}] Hello {target}!", context.CurrentExecution, context.Recurrency.Target);

        return Task.CompletedTask;
    }
}

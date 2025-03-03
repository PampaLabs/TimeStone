namespace TimeStone;


/// <inheritdoc />
public class RecurrenceRunner : IRecurrenceRunner
{
    private readonly IRecurrenceStore _store;
    private readonly IRecurrenceHandlerProvider _provider;

    private TimeProvider _timeProvider = TimeProvider.System;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecurrenceRunner"/> class.
    /// </summary>
    /// <param name="store">The recurrence store used to retrieve recurring tasks.</param>
    /// <param name="provider">The recurrence handler provider used to get the appropriate handlers for tasks.</param>
    public RecurrenceRunner(
        IRecurrenceStore store,
        IRecurrenceHandlerProvider provider
        )
    {
        _store = store;
        _provider = provider;
    }

    /// <inheritdoc />
    public async Task ProcessAsync(CancellationToken cancellationToken = default)
    {
        var executionTime = _timeProvider.GetUtcNow().DateTime;

        var readyTasks = await _store.GetByStatusAsync(RecurringTaskStatus.Ready, cancellationToken);
        readyTasks = readyTasks.ToList();

        while (!cancellationToken.IsCancellationRequested)
        {
            var pendingTaks = readyTasks.Where(x => x.NextExecution <= executionTime);
            var recurrency = pendingTaks.OrderBy(x => x.NextExecution).FirstOrDefault();

            if (recurrency is null) break;

            var currentExecution = recurrency.GetPendingExecutions(executionTime).First();

            var context = new RecurrenceContext
            {
                Recurrency = recurrency,
                ExecutionTime = executionTime,
                CurrentExecution = currentExecution,
            };

            await ProcessAsync(context, cancellationToken);
        }
    }

    /// <inheritdoc />
    private async Task ProcessAsync(IRecurrenceContext context, CancellationToken cancellationToken = default)
    {
        var transactionFactory = new RecurrenceTransactionFactory(_store);
        await using var transaction = await transactionFactory.BeginTransactionAsync(context);

        var handlers = _provider.GetHandlers(context.Recurrency.Name);

        foreach (var handler in handlers)
        {
            await handler.HandleAsync(context, cancellationToken);
        }

        await transaction.CommitAsync();
    }

    /// <summary>
    /// Sets the time provider for obtaining the current time in UTC.
    /// </summary>
    /// <param name="timeProvider">The time provider to use.</param>
    public void UseTimeProvider(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }
}

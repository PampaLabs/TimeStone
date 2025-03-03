namespace TimeStone;

/// <summary>
/// Defines a handler for processing recurrence executions.
/// </summary>
public interface IRecurrenceHandler
{
    /// <summary>
    /// Handles the execution of a recurrence task asynchronously.
    /// </summary>
    /// <param name="context">The context containing recurrence execution details.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task HandleAsync(IRecurrenceContext context, CancellationToken cancellationToken = default);
}

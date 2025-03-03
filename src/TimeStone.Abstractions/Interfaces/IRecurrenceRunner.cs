namespace TimeStone;

/// <summary>
/// Defines a runner responsible for processing scheduled recurrence tasks.
/// </summary>
public interface IRecurrenceRunner
{
    /// <summary>
    /// Processes pending recurrence tasks asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to observe while waiting for the operation to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ProcessAsync(CancellationToken cancellationToken = default);
}

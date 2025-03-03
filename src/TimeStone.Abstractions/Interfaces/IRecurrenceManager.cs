namespace TimeStone;

/// <summary>
/// Defines operations for managing recurring tasks.
/// </summary>
public interface IRecurrenceManager
{
    /// <summary>
    /// Retrieves a recurring task by its name.
    /// </summary>
    /// <param name="name">The name of the recurring task.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the operation to complete.</param>
    /// <returns>The recurring task if found; otherwise, <c>null</c>.</returns>
    Task<RecurringTask?> GetAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a recurring task by its name and target identifier.
    /// </summary>
    /// <param name="name">The name of the recurring task.</param>
    /// <param name="target">The target identifier associated with the recurring task.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the operation to complete.</param>
    /// <returns>The recurring task if found; otherwise, <c>null</c>.</returns>
    Task<RecurringTask?> GetAsync(string name, string target, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new recurring task.
    /// </summary>
    /// <param name="item">The recurring task to create.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the operation to complete.</param>
    Task CreateAsync(RecurringTask item, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing recurring task.
    /// </summary>
    /// <param name="item">The recurring task to update.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the operation to complete.</param>
    Task UpdateAsync(RecurringTask item, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a recurring task.
    /// </summary>
    /// <param name="item">The recurring task to delete.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the operation to complete.</param>
    Task DeleteAsync(RecurringTask item, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a collection of recurring tasks based on their status.
    /// </summary>
    /// <param name="status">The status of the recurring tasks to retrieve.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the operation to complete.</param>
    /// <returns>A collection of recurring tasks that match the specified status.</returns>
    Task<IEnumerable<RecurringTask>> GetByStatusAsync(RecurringTaskStatus status, CancellationToken cancellationToken = default);
}

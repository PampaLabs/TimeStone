namespace TimeStone;

/// <inheritdoc />
public class RecurrenceManager : IRecurrenceManager
{
    private readonly IRecurrenceStore _store;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecurrenceManager"/> class.
    /// </summary>
    /// <param name="store">The recurrence store used for storing and retrieving recurring tasks.</param>
    public RecurrenceManager(IRecurrenceStore store)
    {
        _store = store;
    }

    /// <inheritdoc />
    public async Task<RecurringTask?> GetAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _store.GetAsync(name, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<RecurringTask?> GetAsync(string name, string target, CancellationToken cancellationToken = default)
    {
        return await _store.GetAsync(name, target, cancellationToken);
    }

    /// <inheritdoc />
    public async Task CreateAsync(RecurringTask item, CancellationToken cancellationToken = default)
    {
        item.Reschedule();
        await _store.CreateAsync(item, cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(RecurringTask item, CancellationToken cancellationToken = default)
    {
        item.Reschedule();
        await _store.UpdateAsync(item, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(RecurringTask item, CancellationToken cancellationToken = default)
    {
        item.Reschedule();
        await _store.DeleteAsync(item, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<RecurringTask>> GetByStatusAsync(RecurringTaskStatus status, CancellationToken cancellationToken = default)
    {
        return await GetByStatusAsync(status, cancellationToken);
    }
}

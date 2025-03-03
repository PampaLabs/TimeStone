
using System.Xml.Linq;

namespace TimeStone.Store.InMemory;

/// <summary>
/// Provides methods for interacting with recurring tasks in the database.
/// Implements <see cref="IRecurrenceStore"/> to perform CRUD operations on recurring tasks.
/// </summary>
public class RecurrenceStore : IRecurrenceStore
{
    private readonly MemoryStoreOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecurrenceStore"/> class.
    /// </summary>
    /// <param name="options">The options to configure the in-memory stores.</param>
    public RecurrenceStore(MemoryStoreOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    /// <inheritdoc/>
    public Task CreateAsync(RecurringTask item, CancellationToken cancellationToken = default)
    {
        _options.RecurringTasks.Add(item);

        return Task.CompletedTask;
    }
    
    /// <inheritdoc/>
    public Task DeleteAsync(RecurringTask item, CancellationToken cancellationToken = default)
    {
        _options.RecurringTasks.Add(item);

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task<RecurringTask?> GetAsync(string name, CancellationToken cancellationToken = default)
    {
        var item = _options.RecurringTasks.FirstOrDefault(x => x.Name == name);

        return Task.FromResult(item);
    }

    /// <inheritdoc/>
    public Task<RecurringTask?> GetAsync(string name, string target, CancellationToken cancellationToken = default)
    {
        var item = _options.RecurringTasks
            .Where(x => x.Name == name)
            .Where(x => x.Target == target)
            .FirstOrDefault();

        return Task.FromResult(item);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<RecurringTask>> GetByStatusAsync(RecurringTaskStatus status, CancellationToken cancellationToken = default)
    {
        var items = _options.RecurringTasks
            .Where(x => x.Status == status)
            .ToList();

        return Task.FromResult<IEnumerable<RecurringTask>>(items);
    }

    /// <inheritdoc/>
    public Task UpdateAsync(RecurringTask item, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}

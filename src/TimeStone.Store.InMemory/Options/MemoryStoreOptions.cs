namespace TimeStone.Store.InMemory;

/// <summary>
/// Options for configuring in-memory stores.
/// </summary>
public class MemoryStoreOptions
{
    /// <summary>
    /// Gets or sets the set of recurring tasks in the memory store.
    /// </summary>
    public HashSet<RecurringTask> RecurringTasks { get; set; } = [];
}

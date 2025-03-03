namespace TimeStone;

/// <summary>
/// Represents the context for a recurrence execution, implementing <see cref="IRecurrenceContext"/>.
/// </summary>
internal class RecurrenceContext : IRecurrenceContext
{
    /// <summary>
    /// Gets or sets the recurring task associated with the execution context.
    /// </summary>
    public RecurringTask Recurrency { get; set; } = default!;

    /// <summary>
    /// Gets or sets the scheduled execution time of the recurrence task.
    /// </summary>
    public DateTime ExecutionTime { get; set; }

    /// <summary>
    /// Gets or sets the actual execution time of the current recurrence instance.
    /// </summary>
    public DateTime CurrentExecution { get; set; }
}


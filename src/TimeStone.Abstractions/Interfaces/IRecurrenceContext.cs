namespace TimeStone;

/// <summary>
/// Represents the context for a recurrence execution.
/// </summary>
public interface IRecurrenceContext
{
    /// <summary>
    /// Gets the recurrence details associated with the execution.
    /// </summary>
    RecurringTask Recurrency { get; }

    /// <summary>
    /// Gets the scheduled execution time of the recurrence.
    /// </summary>
    DateTime ExecutionTime { get; }

    /// <summary>
    /// Gets the actual execution time of the current recurrence instance.
    /// </summary>
    DateTime CurrentExecution { get; }
}

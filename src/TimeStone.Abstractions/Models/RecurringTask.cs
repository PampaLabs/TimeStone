using Cronos;

namespace TimeStone;

/// <summary>
/// Represents a recurring task with a specified schedule and execution details.
/// </summary>
public class RecurringTask
{
    /// <summary>
    /// Gets or sets the name of the recurring task.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the target identifier associated with the recurring task.
    /// </summary>
    public string? Target { get; set; }

    /// <summary>
    /// Gets or sets the cron expression representing the schedule for the recurring task.
    /// </summary>
    public CronExpression Cron { get; set; } = default!;

    /// <summary>
    /// Gets or sets the status of the recurring task.
    /// </summary>
    public RecurringTaskStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the start date of the recurring task.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the finish date of the recurring task, if applicable.
    /// </summary>
    public DateTime? FinishDate { get; set; }

    /// <summary>
    /// Gets or sets the last execution time of the recurring task, if applicable.
    /// </summary>
    public DateTime? LastExecution { get; set; }

    /// <summary>
    /// Gets or sets the next execution time of the recurring task, if applicable.
    /// </summary>
    public DateTime? NextExecution { get; set; }
}

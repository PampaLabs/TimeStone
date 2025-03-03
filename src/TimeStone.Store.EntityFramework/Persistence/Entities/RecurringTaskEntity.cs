namespace TimeStone.Store.EntityFramework.Persistence.Entities;

/// <summary>
/// Represents a recurring task entity in the database.
/// This entity is used to store information about tasks that need to be executed on a recurring basis.
/// </summary>
public class RecurringTaskEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the recurring task.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the recurring task.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the target associated with the recurring task.
    /// </summary>
    public string? Target { get; set; }

    /// <summary>
    /// Gets or sets the Cron expression that defines the schedule of the recurring task.
    /// </summary>
    public string Cron { get; set; } = default!;

    /// <summary>
    /// Gets or sets the status of the recurring task.
    /// </summary>
    public RecurringTaskStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the start date of the recurring task.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the finish date of the recurring task.
    /// </summary>
    public DateTime? FinishDate { get; set; }

    /// <summary>
    /// Gets or sets the last execution date of the recurring task.
    /// </summary>
    public DateTime? LastExecution { get; set; }

    /// <summary>
    /// Gets or sets the next execution date of the recurring task.
    /// </summary>
    public DateTime? NextExecution { get; set; }
}

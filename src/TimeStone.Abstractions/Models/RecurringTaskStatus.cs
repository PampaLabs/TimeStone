namespace TimeStone;

/// <summary>
/// Defines the possible statuses for a recurring task.
/// </summary>
public enum RecurringTaskStatus
{
    /// <summary>
    /// The recurring task is ready to be executed.
    /// </summary>
    Ready = 1,

    /// <summary>
    /// The recurring task is currently running.
    /// </summary>
    Running = 2,

    /// <summary>
    /// The recurring task has been disabled and will not execute.
    /// </summary>
    Disabled = 3,

    /// <summary>
    /// The recurring task has been terminated and will no longer be executed.
    /// </summary>
    Terminated = 4,
}

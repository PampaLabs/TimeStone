namespace TimeStone;

/// <summary>
/// Extension methods for the <see cref="RecurringTask"/> class to manage task scheduling and execution.
/// </summary>
public static class RecurringTaskExtensions
{
    /// <summary>
    /// Reschedules the recurrence task by calculating the next execution time based on the provided cron expression.
    /// </summary>
    /// <param name="task">The recurrence task to reschedule.</param>
    /// <param name="lastExecution">An optional parameter for the last execution time of the task. If not provided, it uses the task's current state.</param>
    public static void Reschedule(this RecurringTask task, DateTime? lastExecution = null)
    {
        if (task.Status == RecurringTaskStatus.Disabled) return;

        if (lastExecution.HasValue)
        {
            task.LastExecution = lastExecution.Value;
        }

        var firstRun = !task.LastExecution.HasValue;

        var fromDate = task.LastExecution ?? task.StartDate;
        var toDate = task.Cron.GetNextOccurrence(fromDate, firstRun)!.Value;

        task.NextExecution = toDate <= task.FinishDate ? toDate : null;
    }

    /// <summary>
    /// Gets a collection of pending execution dates for the recurrence task up to the specified time limit.
    /// </summary>
    /// <param name="task">The recurrence task to get pending executions for.</param>
    /// <param name="timeLimit">The time limit to get the pending executions up to.</param>
    /// <returns>An enumerable collection of <see cref="DateTime"/> values representing the pending execution dates.</returns>
    public static IEnumerable<DateTime> GetPendingExecutions(this RecurringTask task, DateTime timeLimit)
    {
        var firstRun = !task.LastExecution.HasValue;

        var fromDate = task.LastExecution ?? task.StartDate;
        var toDate = timeLimit > task.FinishDate ? task.FinishDate.Value : timeLimit;

        return task.Cron.GetOccurrences(
            fromDate,
            toDate,
            fromInclusive: firstRun,
            toInclusive: true);
    }
}


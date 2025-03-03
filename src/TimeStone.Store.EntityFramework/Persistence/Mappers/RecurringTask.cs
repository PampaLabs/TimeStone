using Cronos;

using TimeStone.Store.EntityFramework.Persistence.Entities;

namespace TimeStone.Store.EntityFramework;

internal static partial class EntityConvert
{
    public static RecurringTask FromEntity(RecurringTaskEntity source)
    {
        var target = new RecurringTask();
        FromEntity(source, target);
        return target;
    }

    public static void FromEntity(RecurringTaskEntity source, RecurringTask target)
    {
        target.Name = source.Name;
        target.Target = source.Target;
        target.Cron = CronExpression.Parse(source.Cron);
        target.Status = source.Status;
        target.StartDate = DateTime.SpecifyKind(source.StartDate, DateTimeKind.Utc);

        if (source.FinishDate.HasValue)
        {
            target.FinishDate = DateTime.SpecifyKind(source.FinishDate.Value, DateTimeKind.Utc);
        }

        if (source.LastExecution.HasValue)
        {
            target.LastExecution = DateTime.SpecifyKind(source.LastExecution.Value, DateTimeKind.Utc);
        }

        if (source.NextExecution.HasValue)
        {
            target.NextExecution = DateTime.SpecifyKind(source.NextExecution.Value, DateTimeKind.Utc);
        }
    }

    public static RecurringTaskEntity ToEntity(RecurringTask source)
    {
        var target = new RecurringTaskEntity();
        ToEntity(source, target);
        return target;
    }

    public static void ToEntity(RecurringTask source, RecurringTaskEntity target)
    {
        target.Name = source.Name;
        target.Target = source.Target;
        target.Cron = source.Cron.ToString();
        target.Status = source.Status;
        target.StartDate = source.StartDate;
        target.FinishDate = source.FinishDate;
        target.LastExecution = source.LastExecution;
        target.NextExecution = source.NextExecution;
    }
}

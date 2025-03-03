using System.Reflection;

using Microsoft.EntityFrameworkCore;

using TimeStone.Store.EntityFramework;
using TimeStone.Store.EntityFramework.Persistence;

namespace TimeStone.Tests;

public abstract class BaseTest
{
    protected IRecurrenceStore Store { get; }

    protected IRecurrenceManager Manager { get; }

    protected IRecurrenceHandlerProvider HandlerProvider { get; }

    public BaseTest()
    {
        var context = CreateDbContext();
        Store = new RecurrenceStore(context);

        Manager = new RecurrenceManager(Store);
        HandlerProvider = CreateHandlerProvider();
    }

    private TimeStoneDbContext CreateDbContext()
    {
        var builder = new DbContextOptionsBuilder<TimeStoneDbContext>().UseInMemoryDatabase("TimeStone");
        return new TimeStoneDbContext(builder.Options);
    }

    private IRecurrenceHandlerProvider CreateHandlerProvider()
    {
        var handlers = CreateHandlersFromAssebly();
        var activator = RecurrenceHandlerActivator.FromDefault();

        return new RecurrenceHandlerProvider(handlers, activator);

        static IRecurrenceHandlerCollection CreateHandlersFromAssebly() => new RecurrenceHandlerCollection()
            .RegisterHandlersFromAssembly(Assembly.GetExecutingAssembly());
    }
}

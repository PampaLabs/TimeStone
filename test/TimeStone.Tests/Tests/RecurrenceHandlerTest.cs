using System.Linq.Expressions;

using Cronos;

using FluentAssertions;

using Microsoft.Extensions.Time.Testing;

using Moq;

using Xunit;

namespace TimeStone.Tests;

public class RecurrenceHandlerTest : BaseTest
{
    [Fact]
    public async Task ProcessAsync_WhenTimeAdvances_ShouldTriggerHandlersAtExpectedIntervals()
    {
        var timeProvider = new FakeTimeProvider();

        var recurrenceHandler = new Mock<IRecurrenceHandler>();

        var recurrenceHandlerMethod = (Expression<Func<IRecurrenceHandler, Task>>)
            ((IRecurrenceHandler r) => r.HandleAsync(
                It.IsAny<IRecurrenceContext>(),
                It.IsAny<CancellationToken>()
            ));

        recurrenceHandler.Setup(recurrenceHandlerMethod)
            .Returns(Task.CompletedTask);

        var handlerProvider = new MockRecurrenceHandlerProvider(new() {
            { "Greeting", [recurrenceHandler.Object] }
        });

        var runner = new RecurrenceRunner(Store, handlerProvider);
        runner.UseTimeProvider(timeProvider);

        // Arrange
        await ScheduleTask(timeProvider);

        var count = 3;

        for (int i = 0; i < count; i++)
        {
            timeProvider.Advance(TimeSpan.FromMinutes(5));

            await runner.ProcessAsync();
            recurrenceHandler.Verify(recurrenceHandlerMethod, Times.Exactly(i));
        }

        timeProvider.Advance(TimeSpan.FromMinutes(10));

        await runner.ProcessAsync();
        recurrenceHandler.Verify(recurrenceHandlerMethod, Times.Exactly(count));
    }

    private async Task ScheduleTask(TimeProvider timeProvider)
    {
        var greetingTask = new RecurringTask
        {
            Name = "Greeting",
            Target = "World",
            Cron = CronExpression.Parse("*/5 * * * *"),
            StartDate = timeProvider.GetUtcNow().AddMinutes(6).UtcDateTime,
            FinishDate = timeProvider.GetUtcNow().AddMinutes(20).UtcDateTime,
            Status = RecurringTaskStatus.Ready,
        };

        var manager = new RecurrenceManager(Store);

        await manager.CreateAsync(greetingTask);
    }
}

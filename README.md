# TimeStone

[![CI](https://github.com/PampaLabs/TimeStone/actions/workflows/ci.yml/badge.svg)](https://github.com/PampaLabs/TimeStone/actions/workflows/ci.yml)
[![Downloads](https://img.shields.io/nuget/dt/TimeStone)](https://www.nuget.org/stats/packages/TimeStone?groupby=Version)
[![NuGet](https://img.shields.io/nuget/v/TimeStone)](https://www.nuget.org/packages/TimeStone/)

TimeStone is a library for handling recurring tasks, providing resilience to guarantee the processing of pending executions while preserving their chronology.
It integrates with your application to manage and process tasks that need to be executed on a regular basis.
The library supports scheduling, handling, and executing tasks according to a cron expression.

## Getting Started

### Installation

To install TimeStone, use the following command in your project directory:

```bash
dotnet add package TimeStone
```

Then, you need to register TimeStone services in your application's dependency injection container. In `Startup.cs` or `Program.cs`, add the following:

```csharp
var timeStoneBuilder = builder.Services.AddTimeStone();
```

### Store Configuration

To persist scheduled tasks, TimeStone requires a storage provider. The library supports multiple storage backends, and in this example, we use Entity Framework Core.

First, install the necessary package:

```bash
dotnet add package TimeStone.Store.EntityFramework
```

Then, configure the store provider in your service registration:

```csharp
timeStoneBuilder.AddEntityFrameworkStores(builder =>
{
    builder.UseSqlServer(sqlContainer.GetConnectionString(), options =>
    {
        options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
    });
});
```

### Registering Task Handlers

TimeStone allows you to register recurrence handlers that define how tasks should be processed. Handlers are responsible for executing specific recurring tasks.

You can register a single handler explicitly:

```csharp
timeStoneBuilder.AddHandlers(handlers =>
{
    handlers.AddHandler<GreetingHandler>("Greeting");
});
```

Alternatively, you can register all handlers from an assembly automatically:

```csharp
timeStoneBuilder.AddHandlers(options =>
{
    options.RegisterHandlersFromAssembly(Assembly.GetExecutingAssembly());
});
```

### Creating a Recurring Task

Create a recurring task by defining its cron expression and target execution details. The following example defines a task that executes every minute for the next five minutes:

```csharp
var manager = serviceProvider.GetRequiredService<IRecurrenceManager>();

var greeting = new RecurringTask
{
    Name = "Greeting",
    Target = "World",
    Cron = CronExpression.Parse("* * * * *"),
    StartDate = DateTime.UtcNow,
    FinishDate = DateTime.UtcNow.AddMinutes(5),
    Status = RecurringTaskStatus.Ready,
};

await manager.CreateAsync(greeting);
```

### Implementing a Task Handler

To execute a recurring task, you need to define a handler class that implements `IRecurrenceHandler`. The following example logs a message every time the task runs:

```csharp
[RecurrenceHandler("Greeting")]
public class GreetingHandler : IRecurrenceHandler
{
    private readonly ILogger<GreetingHandler> _logger;

    public GreetingHandler(ILogger<GreetingHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(IRecurrenceContext context, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("[{execution}] Hello {target}!", context.CurrentExecution, context.Recurrency.Target);

        return Task.CompletedTask;
    }
}
```

### Processing Recurring Tasks

The `RecurrenceRunner` is responsible for processing the scheduled recurring tasks. This is typically executed within a background service:

```csharp
public class RecurrenceWorker : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<ConsumerWorker> _logger;

    public RecurrenceWorker(IServiceScopeFactory serviceScopeFactory, ILogger<RecurrenceWorker> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var runner = scope.ServiceProvider.GetRequiredService<IRecurrenceRunner>();

        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
 
            await runner.ProcessAsync();

            await Task.Delay(10_000, stoppingToken);
        }
    }
}
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request on GitHub.

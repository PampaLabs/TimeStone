using System.Reflection;

using ConsoleSample;

using Microsoft.EntityFrameworkCore;

using TimeStone;
using TimeStone.Store.EntityFramework;
using TimeStone.Store.EntityFramework.Persistence;

using Testcontainers.MsSql;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddHostedService<ConsumerWorker>();
builder.Services.AddHostedService<ProducerWorker>();

await using var sqlContainer = new MsSqlBuilder().Build();
await sqlContainer.StartAsync();

builder.Services.AddTimeStone()
    .AddEntityFrameworkStores(builder =>
    {
        builder.UseSqlServer(sqlContainer.GetConnectionString(), options =>
        {
            options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        });
    })
    .AddHandlers(options =>
    {
        options.RegisterHandlersFromAssembly(Assembly.GetExecutingAssembly());
    });

var host = builder.Build();

var context = host.Services.GetRequiredService<TimeStoneDbContext>();
await context.Database.MigrateAsync();

host.Run();

using Microsoft.Extensions.DependencyInjection;

namespace TimeStone.Store.InMemory;

/// <summary>
/// Provides extension methods to configure the TimeStone system in memory stores.
/// </summary>
public static class TimeStoneBuilderExtensions
{
    /// <summary>
    /// Adds in-memory stores for the TimeStone system.
    /// Registers the necessary services for recurring task management.
    /// </summary>
    /// <param name="builder">The <see cref="TimeStoneBuilder"/> instance used for configuration.</param>
    /// <returns>The updated <see cref="TimeStoneBuilder"/> instance with the added in-memory stores.</returns>
    public static TimeStoneBuilder AddInMemoryStores(this TimeStoneBuilder builder)
    {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));

        ConfigureServices(builder.Services);

        return AddInMemoryStores(builder, (sp, options) => { });
    }

    /// <summary>
    /// Adds in-memory stores for the TimeStone system.
    /// Registers the necessary services for recurring task management.
    /// </summary>
    /// <param name="builder">The <see cref="TimeStoneBuilder"/> instance used for configuration.</param>
    /// <param name="optionsAction">An action to configure <see cref="MemoryStoreOptions"/>.</param>
    /// <returns>The updated <see cref="TimeStoneBuilder"/> instance with the added in-memory stores.</returns>
    public static TimeStoneBuilder AddInMemoryStores(this TimeStoneBuilder builder, Action<MemoryStoreOptions> optionsAction)
    {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));

        ConfigureServices(builder.Services);

        builder.Services.AddSingleton(sp => optionsAction.Build());

        return builder;
    }

    /// <summary>
    /// Adds in-memory stores for the TimeStone system.
    /// Registers the necessary services for recurring task management.
    /// </summary>
    /// <param name="builder">The <see cref="TimeStoneBuilder"/> instance used for configuration.</param>
    /// <param name="optionsAction">An action to configure <see cref="MemoryStoreOptions"/>.</param>
    /// <returns>The updated <see cref="TimeStoneBuilder"/> instance with the added in-memory stores.</returns>
    public static TimeStoneBuilder AddInMemoryStores(this TimeStoneBuilder builder, Action<IServiceProvider, MemoryStoreOptions> optionsAction)
    {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));

        ConfigureServices(builder.Services);

        builder.Services.AddSingleton(sp => optionsAction.Build(sp));

        return builder;
    }

    /// <summary>
    /// Registers the necessary stores for recurring tasks.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> used to register the services.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> with the added stores.</returns>
    private static IServiceCollection ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IRecurrenceStore, RecurrenceStore>();

        return services;
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using TimeStone.Store.EntityFramework.Persistence;

namespace TimeStone.Store.EntityFramework;

/// <summary>
/// Provides extension methods for the <see cref="TimeStoneBuilder"/> to integrate with Entity Framework stores and configure the necessary services.
/// </summary>
public static class TimeStoneBuilderExtensions
{
    /// <summary>
    /// Adds entity framework stores and configures the <see cref="TimeStoneDbContext"/> to the service collection.
    /// </summary>
    /// <param name="builder">The <see cref="TimeStoneBuilder"/> instance.</param>
    /// <param name="optionsAction">An optional action to configure the <see cref="DbContextOptionsBuilder"/>.</param>
    /// <returns>The updated <see cref="TimeStoneBuilder"/>.</returns>
    public static TimeStoneBuilder AddEntityFrameworkStores(this TimeStoneBuilder builder, Action<DbContextOptionsBuilder>? optionsAction)
    {
        builder.Services.AddStores();
        builder.Services.AddDbContext<TimeStoneDbContext>(optionsAction);

        return builder;
    }

    /// <summary>
    /// Adds entity framework stores and configures the <see cref="TimeStoneDbContext"/> to the service collection using an <see cref="IServiceProvider"/>.
    /// </summary>
    /// <param name="builder">The <see cref="TimeStoneBuilder"/> instance.</param>
    /// <param name="optionsAction">An optional action to configure the <see cref="DbContextOptionsBuilder"/> with access to the <see cref="IServiceProvider"/>.</param>
    /// <returns>The updated <see cref="TimeStoneBuilder"/>.</returns>
    public static TimeStoneBuilder AddEntityFrameworkStores(this TimeStoneBuilder builder, Action<IServiceProvider, DbContextOptionsBuilder>? optionsAction)
    {
        builder.Services.AddStores();
        builder.Services.AddDbContext<TimeStoneDbContext>(optionsAction);

        return builder;
    }

    /// <summary>
    /// Adds the recurrence store service to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to which the store will be added.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    private static IServiceCollection AddStores(this IServiceCollection services)
    {
        services.AddScoped<IRecurrenceStore, RecurrenceStore>();

        return services;
    }
}

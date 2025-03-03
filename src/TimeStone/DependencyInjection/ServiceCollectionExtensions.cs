using Microsoft.Extensions.DependencyInjection;

namespace TimeStone;

/// <summary>
/// Extension methods for adding TimeStone services to the <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the core services required for the TimeStone framework to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> where services will be registered.</param>
    /// <returns>A <see cref="TimeStoneBuilder"/> instance to allow further configuration of the services.</returns>
    public static TimeStoneBuilder AddTimeStone(this IServiceCollection services)
    {
        services.AddScoped<IRecurrenceManager, RecurrenceManager>();
        services.AddScoped<IRecurrenceRunner, RecurrenceRunner>();

        return new TimeStoneBuilder(services);
    }
}

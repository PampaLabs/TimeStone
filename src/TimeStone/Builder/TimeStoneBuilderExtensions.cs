using Microsoft.Extensions.DependencyInjection;

namespace TimeStone;

/// <summary>
/// Extension methods for configuring services in the <see cref="TimeStoneBuilder"/>.
/// These methods add specific components or services to the <see cref="IServiceCollection"/>.
/// </summary>
public static class TimeStoneBuilderExtensions
{
    /// <summary>
    /// Adds recurrence handlers to the builder and registers a provider for them.
    /// </summary>
    /// <param name="builder">The <see cref="TimeStoneBuilder"/> instance to extend.</param>
    /// <param name="options">An action to configure the collection of recurrence handlers.</param>
    /// <returns>The <see cref="TimeStoneBuilder"/> instance for chaining.</returns>
    public static TimeStoneBuilder AddHandlers(this TimeStoneBuilder builder, Action<IRecurrenceHandlerCollection> options)
    {
        var handlers = new RecurrenceHandlerCollection();
        options.Invoke(handlers);

        builder.Services.AddScoped<IRecurrenceHandlerProvider>(serviceProvider =>
            handlers.BuildHandlerProvider(serviceProvider)
        );

        return builder;
    }
}

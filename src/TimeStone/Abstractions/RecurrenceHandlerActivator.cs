namespace TimeStone;

/// <summary>
/// Provides methods to create instances of <see cref="IRecurrenceHandlerActivator"/> 
/// using different activation mechanisms.
/// </summary>
public static class RecurrenceHandlerActivator
{
    /// <summary>
    /// Creates an instance of <see cref="IRecurrenceHandlerActivator"/> using the default activation method.
    /// </summary>
    /// <returns>An instance of <see cref="IRecurrenceHandlerActivator"/> created by the default method.</returns>
    public static IRecurrenceHandlerActivator FromDefault()
        => new RecurrenceHandlerActivatorDefault();

    /// <summary>
    /// Creates an instance of <see cref="IRecurrenceHandlerActivator"/> using dependency injection with a service provider.
    /// </summary>
    /// <param name="serviceProvider">The service provider used for dependency injection.</param>
    /// <returns>An instance of <see cref="IRecurrenceHandlerActivator"/> created by the service provider.</returns>
    public static IRecurrenceHandlerActivator FromServiceProvider(IServiceProvider serviceProvider)
        => new RecurrenceHandlerActivatorDependencyInjection(serviceProvider);
}

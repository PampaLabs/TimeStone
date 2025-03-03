using Microsoft.Extensions.DependencyInjection;

namespace TimeStone;

/// <summary>
/// Provides an implementation of <see cref="IRecurrenceHandlerActivator"/> that uses dependency injection
/// via <see cref="IServiceProvider"/> to create instances of recurrence handlers.
/// </summary>
internal class RecurrenceHandlerActivatorDependencyInjection : IRecurrenceHandlerActivator
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecurrenceHandlerActivatorDependencyInjection"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider used for dependency injection.</param>
    public RecurrenceHandlerActivatorDependencyInjection(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Creates an instance of the specified recurrence handler type using dependency injection.
    /// </summary>
    /// <param name="instanceType">The type of the recurrence handler to create.</param>
    /// <returns>An instance of the specified recurrence handler type.</returns>
    public object CreateInstance(Type instanceType)
        => ActivatorUtilities.CreateInstance(_serviceProvider, instanceType);

    /// <summary>
    /// Creates an instance of a recurrence handler of the specified generic type using dependency injection.
    /// </summary>
    /// <typeparam name="T">The type of the recurrence handler to create, which must implement <see cref="IRecurrenceHandler"/>.</typeparam>
    /// <returns>An instance of the specified recurrence handler type.</returns>
    public T CreateInstance<T>() where T : IRecurrenceHandler
        => ActivatorUtilities.CreateInstance<T>(_serviceProvider);
}

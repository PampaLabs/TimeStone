namespace TimeStone;

/// <summary>
/// Provides the default implementation of <see cref="IRecurrenceHandlerActivator"/> for creating
/// instances of recurrence handlers using <see cref="Activator"/>.
/// </summary>
internal class RecurrenceHandlerActivatorDefault : IRecurrenceHandlerActivator
{
    /// <summary>
    /// Creates an instance of the specified recurrence handler type.
    /// </summary>
    /// <param name="instanceType">The type of the recurrence handler to create.</param>
    /// <returns>An instance of the specified recurrence handler type.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="instanceType"/> is null.</exception>
    public object CreateInstance(Type instanceType)
        => Activator.CreateInstance(instanceType)!;

    /// <summary>
    /// Creates an instance of a recurrence handler of the specified generic type.
    /// </summary>
    /// <typeparam name="T">The type of the recurrence handler to create, which must implement <see cref="IRecurrenceHandler"/>.</typeparam>
    /// <returns>An instance of the specified recurrence handler type.</returns>
    public T CreateInstance<T>() where T : IRecurrenceHandler
        => Activator.CreateInstance<T>();
}

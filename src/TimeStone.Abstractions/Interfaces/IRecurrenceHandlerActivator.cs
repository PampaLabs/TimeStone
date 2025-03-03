namespace TimeStone;

/// <summary>
/// Provides a mechanism for creating instances of recurrence handlers.
/// </summary>
public interface IRecurrenceHandlerActivator
{
    /// <summary>
    /// Creates an instance of the specified recurrence handler type.
    /// </summary>
    /// <param name="instanceType">The type of the recurrence handler to create.</param>
    /// <returns>An instance of the specified type.</returns>
    object CreateInstance(Type instanceType);

    /// <summary>
    /// Creates an instance of a recurrence handler of the specified generic type.
    /// </summary>
    /// <typeparam name="T">The type of the recurrence handler, which must implement <see cref="IRecurrenceHandler"/>.</typeparam>
    /// <returns>An instance of the specified recurrence handler type.</returns>
    T CreateInstance<T>() where T : IRecurrenceHandler;
}

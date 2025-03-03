namespace TimeStone;

/// <summary>
/// Provides extension methods for adding recurrence handlers to an <see cref="IRecurrenceHandlerCollection"/>.
/// </summary>
public static class RecurrenceHandlerCollectionExtensions
{
    /// <summary>
    /// Adds a recurrence handler to the collection using a generic type parameter.
    /// </summary>
    /// <typeparam name="T">The type of the recurrence handler, which must implement <see cref="IRecurrenceHandler"/>.</typeparam>
    /// <param name="handlers">The collection to which the handler will be added.</param>
    /// <param name="name">The name identifying the recurrence handler.</param>
    /// <returns>The updated <see cref="IRecurrenceHandlerCollection"/>.</returns>
    public static IRecurrenceHandlerCollection AddHandler<T>(this IRecurrenceHandlerCollection handlers, string name)
        where T : IRecurrenceHandler
    {
        return handlers.AddHandler(name, typeof(T));
    }

    /// <summary>
    /// Adds a recurrence handler to the collection using a specified handler type.
    /// </summary>
    /// <param name="handlers">The collection to which the handler will be added.</param>
    /// <param name="name">The name identifying the recurrence handler.</param>
    /// <param name="handlerType">The type of the recurrence handler.</param>
    /// <returns>The updated <see cref="IRecurrenceHandlerCollection"/>.</returns>
    public static IRecurrenceHandlerCollection AddHandler(this IRecurrenceHandlerCollection handlers, string name, Type handlerType)
    {
        handlers.Add(new(name, handlerType));
        return handlers;
    }
}

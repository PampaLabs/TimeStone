namespace TimeStone;

/// <summary>
/// Provides a mechanism to retrieve recurrence handlers by name.
/// </summary>
public interface IRecurrenceHandlerProvider
{
    /// <summary>
    /// Retrieves a collection of recurrence handlers associated with the specified name.
    /// </summary>
    /// <param name="name">The name identifying the recurrence handlers.</param>
    /// <returns>A collection of <see cref="IRecurrenceHandler"/> instances associated with the given name.</returns>
    IEnumerable<IRecurrenceHandler> GetHandlers(string name);
}

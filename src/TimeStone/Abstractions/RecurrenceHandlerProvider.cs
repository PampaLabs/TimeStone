namespace TimeStone;


/// <inheritdoc />
public class RecurrenceHandlerProvider : IRecurrenceHandlerProvider
{
    private readonly IRecurrenceHandlerCollection _collection;
    private readonly IRecurrenceHandlerActivator _activator;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecurrenceHandlerProvider"/> class.
    /// </summary>
    /// <param name="collection">The collection of recurrence handler descriptors.</param>
    /// <param name="activator">The activator used to create instances of recurrence handlers.</param>
    public RecurrenceHandlerProvider(IRecurrenceHandlerCollection collection, IRecurrenceHandlerActivator activator)
    {
        _collection = collection;
        _activator = activator;
    }


    /// <inheritdoc />
    public IEnumerable<IRecurrenceHandler> GetHandlers(string name)
    {
        // Get handler types from the collection matching the name
        var handlerTypes = _collection.Where(x => x.Name == name).Select(x => x.HandlerType);

        // Create and yield the handler instances using the activator
        foreach (var handlerType in handlerTypes)
        {
            yield return (IRecurrenceHandler)_activator.CreateInstance(handlerType);
        }
    }
}

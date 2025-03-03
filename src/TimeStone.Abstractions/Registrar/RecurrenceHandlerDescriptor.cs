namespace TimeStone;

/// <summary>
/// Represents a descriptor for a recurrence handler, including its name and type.
/// </summary>
public class RecurrenceHandlerDescriptor
{
    /// <summary>
    /// Gets the name identifying the recurrence handler.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the type of the recurrence handler.
    /// </summary>
    public Type HandlerType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RecurrenceHandlerDescriptor"/> class.
    /// </summary>
    /// <param name="name">The name identifying the recurrence handler.</param>
    /// <param name="handlerType">The type of the recurrence handler.</param>
    public RecurrenceHandlerDescriptor(string name, Type handlerType)
    {
        Name = name;
        HandlerType = handlerType;
    }
}

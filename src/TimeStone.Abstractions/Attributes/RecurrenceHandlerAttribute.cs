namespace TimeStone;

/// <summary>
/// Attribute used to mark a class as a recurrence handler.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class RecurrenceHandlerAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RecurrenceHandlerAttribute"/> class.
    /// </summary>
    /// <param name="name">The name identifying the recurrence handler.</param>
    public RecurrenceHandlerAttribute(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Gets the name identifying the recurrence handler.
    /// </summary>
    public string Name { get; }
}

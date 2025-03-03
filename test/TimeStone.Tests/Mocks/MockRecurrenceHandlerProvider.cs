namespace TimeStone.Tests;

public class MockRecurrenceHandlerProvider : IRecurrenceHandlerProvider
{
    private readonly IDictionary<string, IEnumerable<IRecurrenceHandler>> _dictionary;

    public MockRecurrenceHandlerProvider(Dictionary<string, IEnumerable<IRecurrenceHandler>> dictionary)
    {
        _dictionary = dictionary;
    }

    public IEnumerable<IRecurrenceHandler> GetHandlers(string name)
        => _dictionary[name];
}

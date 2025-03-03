namespace TimeStone;

internal class RecurrenceTransactionFactory
{
    private readonly IRecurrenceStore _store;

    public RecurrenceTransactionFactory(IRecurrenceStore store)
    {
        _store = store;
    }

    public async Task<RecurrenceTransaction> BeginTransactionAsync(IRecurrenceContext context)
    {
        var transaction = new RecurrenceTransaction(_store, context);
        await transaction.InitAsync();
        return transaction;
    }
}

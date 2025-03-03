namespace TimeStone;

internal class RecurrenceTransaction : Transaction
{
    private readonly IRecurrenceStore _store;
    private readonly IRecurrenceContext _context;

    public RecurrenceTransaction(IRecurrenceStore store, IRecurrenceContext context)
    {
        _store = store;
        _context = context;
    }

    public async Task InitAsync()
    {
        if (_context.Recurrency.Status != RecurringTaskStatus.Ready)
        {
            throw new Exception("The recurrency is not ready.");
        }

        _context.Recurrency.Status = RecurringTaskStatus.Running;

        await _store.UpdateAsync(_context.Recurrency);
    }

    protected override async Task ExecuteCommitAsync()
    {
        if (_context.Recurrency.Status != RecurringTaskStatus.Running)
        {
            throw new Exception("The recurrency is not running.");
        }

        _context.Recurrency.Reschedule(_context.CurrentExecution);

        _context.Recurrency.Status = _context.Recurrency.NextExecution.HasValue
            ? RecurringTaskStatus.Ready
            : RecurringTaskStatus.Terminated;

        await _store.UpdateAsync(_context.Recurrency);
    }

    protected override async Task ExecuteRollbackAsync()
    {
        if (_context.Recurrency.Status != RecurringTaskStatus.Running)
        {
            throw new Exception("The recurrency is not running.");
        }

        _context.Recurrency.Status = RecurringTaskStatus.Ready;

        await _store.UpdateAsync(_context.Recurrency);
    }
}

namespace TimeStone;

/// <summary>
/// Abstract base class representing a transaction that can be committed or rolled back asynchronously.
/// </summary>
internal abstract class Transaction : ITransaction
{
    private readonly SemaphoreSlim _semaphore = new(1);

    private bool _completed = false;

    /// <summary>
    /// Executes the commit operation asynchronously. This method must be implemented by a derived class.
    /// </summary>
    /// <returns>A task that represents the asynchronous commit operation.</returns>
    protected abstract Task ExecuteCommitAsync();

    /// <summary>
    /// Executes the rollback operation asynchronously. This method must be implemented by a derived class.
    /// </summary>
    /// <returns>A task that represents the asynchronous rollback operation.</returns>
    protected abstract Task ExecuteRollbackAsync();

    /// <summary>
    /// Commits the transaction asynchronously, applying all changes made during the transaction.
    /// </summary>
    /// <exception cref="TransactionCompletedException">Thrown if the transaction has already been completed (committed or rolled back).</exception>
    /// <returns>A task that represents the asynchronous commit operation.</returns>
    public async Task CommitAsync()
    {
        await _semaphore.WaitAsync();

        if (_completed)
        {
            throw new TransactionCompletedException();
        }

        await ExecuteCommitAsync();

        _completed = true;

        _semaphore.Release();
    }

    /// <summary>
    /// Rolls back the transaction asynchronously, undoing all changes made during the transaction.
    /// </summary>
    /// <exception cref="TransactionCompletedException">Thrown if the transaction has already been completed (committed or rolled back).</exception>
    /// <returns>A task that represents the asynchronous rollback operation.</returns>
    public async Task RollbackAsync()
    {
        await _semaphore.WaitAsync();

        if (_completed)
        {
            throw new TransactionCompletedException();
        }

        await ExecuteRollbackAsync();

        _completed = true;

        _semaphore.Release();
    }

    /// <summary>
    /// Asynchronously disposes of the transaction, ensuring any necessary rollback is performed if the transaction was not completed.
    /// </summary>
    /// <returns>A task that represents the asynchronous dispose operation.</returns>
    public async ValueTask DisposeAsync()
    {
        if (!_completed)
        {
            await RollbackAsync();
        }
    }
}

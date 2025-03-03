namespace TimeStone;

/// <summary>
/// Represents a transaction that can be committed or rolled back asynchronously.
/// </summary>
internal interface ITransaction : IAsyncDisposable
{
    /// <summary>
    /// Commits the transaction asynchronously, applying all changes made during the transaction.
    /// </summary>
    /// <returns>A task that represents the asynchronous commit operation.</returns>
    Task CommitAsync();

    /// <summary>
    /// Rolls back the transaction asynchronously, undoing all changes made during the transaction.
    /// </summary>
    /// <returns>A task that represents the asynchronous rollback operation.</returns>
    Task RollbackAsync();

    /// <summary>
    /// Asynchronously disposes of the transaction, ensuring any necessary rollback is performed.
    /// </summary>
    /// <returns>A task that represents the asynchronous dispose operation.</returns>
    async ValueTask IAsyncDisposable.DisposeAsync() => await RollbackAsync();
}


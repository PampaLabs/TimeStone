namespace TimeStone;

/// <summary>
/// Exception that is thrown when an operation is attempted on a transaction that has already been completed (either committed or rolled back).
/// </summary>
public class TransactionCompletedException : InvalidOperationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionCompletedException"/> class.
    /// </summary>
    public TransactionCompletedException()
        : base("This transaction has completed; it is no longer usable.") { }
}


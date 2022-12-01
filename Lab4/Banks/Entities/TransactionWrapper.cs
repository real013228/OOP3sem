using Banks.Abstractions;

namespace Banks.Entities;

public class TransactionWrapper
{
    private readonly Action _cancelTransaction;
    public TransactionWrapper(ITransaction transaction, Action cancelTransaction)
    {
        Transaction = transaction;
        _cancelTransaction = cancelTransaction;
    }

    public ITransaction Transaction { get; }

    public void CancelTransaction()
    {
        _cancelTransaction();
    }
}
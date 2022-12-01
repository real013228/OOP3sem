using System.Data.Common;
using Banks.Abstractions;

namespace Banks.Entities;

public class TransactionWrapper : IEquatable<TransactionWrapper>
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

    public bool Equals(TransactionWrapper? other)
    {
        return other != null && Transaction.Id == other.Transaction.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_cancelTransaction, Transaction);
    }
}
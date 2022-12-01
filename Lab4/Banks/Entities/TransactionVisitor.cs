using Banks.Abstractions;
using Banks.Entities.Transactions;

namespace Banks.Entities;

public class TransactionVisitor : ITransactionVisitor
{
    public void Visit(DecreaseMoney transaction)
    {
        transaction.Account.TakeMoney(transaction.Value);
    }

    public void Visit(IncreaseMoney transaction)
    {
        transaction.Account.TopUpMoney(transaction.Value);
    }

    public void Visit(TransferMoney transaction)
    {
        transaction.FromAccount.TakeMoney(transaction.Value);
        transaction.ToAccount.TopUpMoney(transaction.Value);
    }
}
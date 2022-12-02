using Banks.Abstractions;
using Banks.Entities.Transactions;
using Banks.Models;

namespace Banks.Entities;

public class TransactionVisitor : ITransactionVisitor
{
    private readonly List<IBankAccount> _accounts;

    public TransactionVisitor(List<IBankAccount> accounts)
    {
        _accounts = accounts;
    }

    public TransactionWrapper? Transaction { get; private set; }

    public void Visit(DecreaseMoney transaction)
    {
        IBankAccount account = _accounts.First(x => x.Id == transaction.Account);
        decimal tookMoney = account.TakeMoney(transaction.Value);
        void Cancel() => account.AccrualMoney(tookMoney);
        var transactionWrapper = new TransactionWrapper(transaction, Cancel);
        Transaction = transactionWrapper;
    }

    public void Visit(IncreaseMoney transaction)
    {
        IBankAccount account = _accounts.First(x => x.Id == transaction.Account);
        decimal topUpMoney = account.TopUpMoney(transaction.Value);
        void Cancel() => account.DecreaseMoney(topUpMoney);
        var transactionWrapper = new TransactionWrapper(transaction, Cancel);
        Transaction = transactionWrapper;
    }

    public void Visit(TransferMoney transaction)
    {
        IBankAccount fromAccount = _accounts.First(x => x.Id == transaction.FromAccount);
        IBankAccount toAccount = _accounts.First(x => x.Id == transaction.ToAccount);
        if (fromAccount.CanTakeMoney(transaction.Value) && toAccount.CanTopUpMoney(transaction.Value))
        {
            decimal takeMoney = fromAccount.TakeMoney(transaction.Value);
            decimal topUpMoney = toAccount.TopUpMoney(transaction.Value);

            var action = new Action(() =>
            {
                fromAccount.AccrualMoney(takeMoney);
                toAccount.DecreaseMoney(topUpMoney);
            });
            Transaction = new TransactionWrapper(transaction, action);
        }
        else
        {
            throw new NullReferenceException();
        }
    }
}
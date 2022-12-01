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
        Balance accountBalance = account.BalanceValue;
        void Cancel() => accountBalance.IncreaseMoney(tookMoney);
        var transactionWrapper = new TransactionWrapper(transaction, Cancel);
        Transaction = transactionWrapper;
    }

    public void Visit(IncreaseMoney transaction)
    {
        IBankAccount account = _accounts.First(x => x.Id == transaction.Account);
        decimal topUpMoney = account.TopUpMoney(transaction.Value);
        Balance accountBalance = account.BalanceValue;
        void Cancel() => accountBalance.DecreaseMoney(topUpMoney);
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
            Balance fromAccountBalance = fromAccount.BalanceValue;
            Balance toAccountBalance = toAccount.BalanceValue;

            var action = new Action(() =>
            {
                fromAccountBalance.IncreaseMoney(takeMoney);
                toAccountBalance.DecreaseMoney(topUpMoney);
            });
            Transaction = new TransactionWrapper(transaction, action);
        }

        throw new NullReferenceException();
    }
}
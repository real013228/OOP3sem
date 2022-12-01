using Banks.Abstractions;

namespace Banks.Entities.Transactions;

public class TransferMoney : ITransaction
{
    public TransferMoney(decimal value, IBankAccount fromAccount,  IBankAccount toAccount)
    {
        Value = value;
        ToAccount = toAccount;
        FromAccount = fromAccount;
        Id = Guid.NewGuid();
    }

    public IBankAccount FromAccount { get; }
    public IBankAccount ToAccount { get; }
    public decimal Value { get; }

    public Guid Id { get; }
    public void Accept(ITransactionVisitor visitor)
    {
        visitor.Visit(this);
    }
}
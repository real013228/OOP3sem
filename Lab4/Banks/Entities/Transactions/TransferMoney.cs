using Banks.Abstractions;

namespace Banks.Entities.Transactions;

public class TransferMoney : ITransaction
{
    public TransferMoney(decimal value, Guid fromAccount,  Guid toAccount)
    {
        Value = value;
        ToAccount = toAccount;
        FromAccount = fromAccount;
        Id = Guid.NewGuid();
    }

    public Guid FromAccount { get; }
    public Guid ToAccount { get; }
    public decimal Value { get; }

    public Guid Id { get; }
    public void Accept(ITransactionVisitor visitor)
    {
        visitor.Visit(this);
    }
}
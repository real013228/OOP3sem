using Banks.Abstractions;

namespace Banks.Entities.Transactions;

public class DecreaseMoney : ITransaction
{
    public DecreaseMoney(decimal value, IBankAccount account)
    {
        Value = value;
        Account = account;
        Id = Guid.NewGuid();
    }

    public IBankAccount Account { get; }
    public decimal Value { get; }

    public Guid Id { get; }
    public void Accept(ITransactionVisitor visitor)
    {
        visitor.Visit(this);
    }
}
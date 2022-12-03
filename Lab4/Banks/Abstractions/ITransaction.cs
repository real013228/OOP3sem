namespace Banks.Abstractions;

public interface ITransaction
{
    Guid Id { get; }
    void Accept(ITransactionVisitor visitor);
}
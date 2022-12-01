using Banks.Entities.Account;
using Banks.Entities.Transactions;

namespace Banks.Abstractions;

public interface ITransactionVisitor
{
    void Visit(DecreaseMoney transaction);
    void Visit(IncreaseMoney transaction);
    void Visit(TransferMoney transaction);
}
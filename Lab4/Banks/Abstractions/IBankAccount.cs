using Banks.Entities;

namespace Banks.Abstractions;

public interface IBankAccount
{
    Client ClientAccount { get; }
    decimal Percent { get; }
    decimal Commission { get; }
    decimal Account { get; }
    Guid Id { get; }
    void TakeMoney(decimal value);
    void TopUpMoney(decimal value);
    void TransferMoney(IBankAccount otherClientAccount, decimal value);
}
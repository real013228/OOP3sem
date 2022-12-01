using Banks.Entities;
using Banks.Models;

namespace Banks.Abstractions;

public interface IBankAccount
{
    Client ClientAccount { get; }
    decimal TransactionLimit { get; set; }
    Balance BalanceValue { get; }
    Guid Id { get; }
    IClock Clock { get; }
    void TakeMoney(decimal value);
    void TopUpMoney(decimal value);
}
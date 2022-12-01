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
    decimal TakeMoney(decimal value);
    decimal TopUpMoney(decimal value);
    bool CanTakeMoney(decimal value);
    bool CanTopUpMoney(decimal value);
}
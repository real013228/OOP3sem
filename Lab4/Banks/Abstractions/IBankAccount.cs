using System.Runtime.CompilerServices;
using Banks.Entities;
using Banks.Models;

namespace Banks.Abstractions;

public interface IBankAccount
{
    INotifyStrategy Notifier { get; set; }
    Client ClientAccount { get; }
    decimal TransactionLimit { get; }
    decimal BalanceValue { get; }
    Guid Id { get; }
    IClock Clock { get; }
    decimal TakeMoney(decimal value);
    decimal TopUpMoney(decimal value);
    bool CanTakeMoney(decimal value);
    bool CanTopUpMoney(decimal value);
    void AccrualMoney(decimal value);
    void DecreaseMoney(decimal value);
}
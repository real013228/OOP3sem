﻿using Banks.Abstractions;
using Banks.Models;

namespace Banks.Entities.Account;

public class DebitAccount : IBankAccount
{
    public DebitAccount(decimal percent, decimal account, Client clientAccount, IClock clock)
    {
        Percent = percent;
        Commission = 0;
        BalanceValue = new Balance(account);
        ClientAccount = clientAccount;
        Clock = clock;
        Id = Guid.NewGuid();
    }

    public Client ClientAccount { get; }
    public decimal TransactionLimit { get; set; }
    public decimal Percent { get; }
    public decimal Commission { get; }
    public Balance BalanceValue { get; }
    public Guid Id { get; }
    public IClock Clock { get; }

    public decimal TakeMoney(decimal value)
    {
        if (!CanTakeMoney(value))
            throw new NullReferenceException();
        return BalanceValue.DecreaseMoney(value);
    }

    public decimal TopUpMoney(decimal value)
    {
        if (!CanTopUpMoney(value))
            throw new NullReferenceException();
        return BalanceValue.IncreaseMoney(value);
    }

    public bool CanTakeMoney(decimal value)
    {
        return !ClientAccount.IsSus || TransactionLimit >= value || BalanceValue.Value >= value;
    }

    public bool CanTopUpMoney(decimal value)
    {
        return !ClientAccount.IsSus || TransactionLimit >= value;
    }
}
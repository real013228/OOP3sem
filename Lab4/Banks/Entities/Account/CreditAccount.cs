using Banks.Abstractions;
using Banks.Entities.AccountCreators;
using Banks.Models;

namespace Banks.Entities.Account;

public class CreditAccount : IBankAccount
{
    public CreditAccount(decimal commission, decimal account, Client clientAccount, IClock clock, decimal creditLimit)
    {
        Percent = 0;
        Commission = commission;
        BalanceValue = new Balance(account);
        ClientAccount = clientAccount;
        Clock = clock;
        CreditLimit = creditLimit;
        Id = Guid.NewGuid();
    }

    public Client ClientAccount { get; }
    public decimal CreditLimit { get; set; }
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
        return BalanceValue.Value < 0
            ? BalanceValue.DecreaseMoney(value + Commission)
            : BalanceValue.DecreaseMoney(value);
    }

    public decimal TopUpMoney(decimal value)
    {
        if (!CanTopUpMoney(value))
            throw new NullReferenceException();
        return BalanceValue.Value < 0
            ? BalanceValue.IncreaseMoney(value - Commission)
            : BalanceValue.IncreaseMoney(value);
    }

    public bool CanTakeMoney(decimal value)
    {
        return (!ClientAccount.IsSus || TransactionLimit >= value) && BalanceValue.Value - value - Commission >= CreditLimit;
    }

    public bool CanTopUpMoney(decimal value)
    {
        return !ClientAccount.IsSus || value <= TransactionLimit;
    }
}
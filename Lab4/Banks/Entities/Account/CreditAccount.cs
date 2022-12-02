using Banks.Abstractions;
using Banks.Entities.AccountCreators;
using Banks.Models;

namespace Banks.Entities.Account;

public class CreditAccount : IBankAccount
{
    private Balance _balanceValue;
    public CreditAccount(decimal commission, decimal account, Client clientAccount, IClock clock, decimal creditLimit)
    {
        Percent = 0;
        Commission = commission;
        _balanceValue = new Balance(account);
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
    public decimal BalanceValue => _balanceValue.Value;
    public Guid Id { get; }
    public IClock Clock { get; }

    public decimal TakeMoney(decimal value)
    {
        if (!CanTakeMoney(value))
            throw new NullReferenceException();
        return _balanceValue.Value < 0
            ? _balanceValue.DecreaseMoney(value + Commission)
            : _balanceValue.DecreaseMoney(value);
    }

    public decimal TopUpMoney(decimal value)
    {
        if (!CanTopUpMoney(value))
            throw new NullReferenceException();
        return _balanceValue.Value < 0
            ? _balanceValue.IncreaseMoney(value - Commission)
            : _balanceValue.IncreaseMoney(value);
    }

    public bool CanTakeMoney(decimal value)
    {
        return (!ClientAccount.IsSus || TransactionLimit >= value) && _balanceValue.Value - value - Commission >= CreditLimit;
    }

    public bool CanTopUpMoney(decimal value)
    {
        return !ClientAccount.IsSus || value <= TransactionLimit;
    }

    public void AccrualMoney(decimal value)
    {
        if (CanTopUpMoney(value))
            _balanceValue.IncreaseMoney(value);
    }

    public void DecreaseMoney(decimal value)
    {
        if (CanTakeMoney(value))
            _balanceValue.DecreaseMoney(value);
    }
}
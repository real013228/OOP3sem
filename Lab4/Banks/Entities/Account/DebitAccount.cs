using Banks.Abstractions;
using Banks.Models;

namespace Banks.Entities.Account;

public class DebitAccount : IBankAccount
{
    private Balance _balanceValue;

    public DebitAccount(decimal percent, decimal account, Client clientAccount, IClock clock)
    {
        Percent = percent;
        Commission = 0;
        _balanceValue = new Balance(account);
        ClientAccount = clientAccount;
        Clock = clock;
        Id = Guid.NewGuid();
    }

    public Client ClientAccount { get; }
    public decimal TransactionLimit { get; set; }
    public decimal Percent { get; }
    public decimal Commission { get; }
    public decimal BalanceValue => -_balanceValue.Value;
    public Guid Id { get; }
    public IClock Clock { get; }

    public decimal TakeMoney(decimal value)
    {
        if (!CanTakeMoney(value))
            throw new NullReferenceException();
        return _balanceValue.DecreaseMoney(value);
    }

    public decimal TopUpMoney(decimal value)
    {
        if (!CanTopUpMoney(value))
            throw new NullReferenceException();
        return _balanceValue.IncreaseMoney(value);
    }

    public bool CanTakeMoney(decimal value)
    {
        return !ClientAccount.IsSus || TransactionLimit >= value || _balanceValue.Value >= value;
    }

    public bool CanTopUpMoney(decimal value)
    {
        return !ClientAccount.IsSus || TransactionLimit >= value;
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
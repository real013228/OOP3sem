using Banks.Abstractions;
using Banks.Models;

namespace Banks.Entities.Account;

public class DepositAccount : IBankAccount
{
    private bool _isExpired;
    private decimal _cashBack;

    public DepositAccount(decimal percent, decimal startAccount, Client clientAccount, IClock clock)
    {
        _isExpired = false;
        Percent = percent;
        Commission = 0;
        BalanceValue = new Balance(startAccount);
        ClientAccount = clientAccount;
        Clock = clock;
        Id = Guid.NewGuid();
        _cashBack = 0;
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
        return (!ClientAccount.IsSus || TransactionLimit >= value) && !_isExpired && !(BalanceValue.Value < value);
    }

    public bool CanTopUpMoney(decimal value)
    {
        return !ClientAccount.IsSus || TransactionLimit >= value;
    }

    private void Verify()
    {
        _isExpired = true;
    }

    private void SetMoneyEveryDay()
    {
        _cashBack += BalanceValue.Value * Percent;
    }

    private void IncreaseMoneyEveryMonth()
    {
        BalanceValue.IncreaseMoney(_cashBack);
    }
}
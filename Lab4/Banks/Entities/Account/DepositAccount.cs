using System.Timers;
using Banks.Abstractions;
using Banks.Models;

namespace Banks.Entities.Account;

public class DepositAccount : IBankAccount
{
    private readonly Balance _balanceValue;
    private bool _isExpired;
    private decimal _cashBack;

    public DepositAccount(decimal percent, decimal startAccount, Client clientAccount, IClock clock, TimeSpan interval)
    {
        _isExpired = false;
        Percent = percent;
        Commission = 0;
        _balanceValue = new Balance(startAccount);
        ClientAccount = clientAccount;
        Clock = clock;
        Id = Guid.NewGuid();
        _cashBack = 0;
        Interval = interval;
        clock.TimeHasBeenExpired += Verify;
        clock.DayHasBeenPassed += SetMoneyEveryDay;
        clock.MonthHasBeenPassed += IncreaseMoneyEveryMonth;
    }

    public Client ClientAccount { get; }
    public decimal TransactionLimit { get; set; }
    public decimal Percent { get; }
    public decimal Commission { get; }
    public decimal BalanceValue => _balanceValue.Value;
    public Guid Id { get; }
    public IClock Clock { get; }
    public TimeSpan Interval { get; }

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
        return (!ClientAccount.IsSus || TransactionLimit >= value) && !_isExpired && !(_balanceValue.Value < value);
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

    private void Verify()
    {
        _isExpired = true;
    }

    private void SetMoneyEveryDay()
    {
        _cashBack += _balanceValue.Value * Percent;
    }

    private void IncreaseMoneyEveryMonth()
    {
        _balanceValue.IncreaseMoney(_cashBack);
    }
}
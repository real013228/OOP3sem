using Banks.Abstractions;
using Banks.Entities.AccountCreators;
using Banks.Models;

namespace Banks.Entities.Account;

public class CreditAccount : IBankAccount
{
    private readonly Balance _balanceValue;
    private readonly Bank _bank;

    public CreditAccount(decimal commission, decimal account, Client clientAccount, IClock clock, decimal creditLimit, Bank bank, decimal transactionLimit, INotifyStrategy notifier)
    {
        Percent = 0;
        Commission = commission;
        _balanceValue = new Balance(account);
        ClientAccount = clientAccount;
        Clock = clock;
        CreditLimit = creditLimit;
        _bank = bank;
        TransactionLimit = transactionLimit;
        Notifier = notifier;
        Id = Guid.NewGuid();
        _bank.CommissionHasBeenChanged += SetCommission;
        _bank.TransactionLimitHasBeenChanged += SetTransactionLimit;
        _bank.CreditLimitHasBeenChanged += SetCreditLimit;
    }

    public INotifyStrategy Notifier { get; set; }
    public Client ClientAccount { get; }
    public decimal CreditLimit { get; private set; }
    public decimal TransactionLimit { get; private set; }
    public decimal Percent { get; }
    public decimal Commission { get; private set; }
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
        return (!ClientAccount.IsSus || TransactionLimit >= value) &&
               _balanceValue.Value - value - Commission >= CreditLimit;
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

    private void SetCommission(decimal value)
    {
        Commission = value;
        Notifier.Notify("Commission has been changed");
    }

    private void SetTransactionLimit(decimal value)
    {
        TransactionLimit = value;
        Notifier.Notify("Transaction limit has been changed");
    }

    private void SetCreditLimit(decimal value)
    {
        CreditLimit = value;
        Notifier.Notify("Credit limit has been changed");
    }
}
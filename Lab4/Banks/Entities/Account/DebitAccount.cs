using Banks.Abstractions;
using Banks.Models;

namespace Banks.Entities.Account;

public class DebitAccount : IBankAccount
{
    private readonly Balance _balanceValue;
    private readonly Bank _bank;
    private decimal _cashBack;

    public DebitAccount(decimal percent, decimal account, Client clientAccount, IClock clock, INotifyStrategy notifier, decimal transactionLimit, Bank bank)
    {
        Percent = percent;
        _balanceValue = new Balance(account);
        ClientAccount = clientAccount;
        Clock = clock;
        Notifier = notifier;
        TransactionLimit = transactionLimit;
        _bank = bank;
        Id = Guid.NewGuid();
        _bank.TransactionLimitHasBeenChanged += SetTransactionLimit;
    }

    public INotifyStrategy Notifier { get; set; }
    public Client ClientAccount { get; }
    public decimal TransactionLimit { get; private set; }
    public decimal Percent { get; }
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

    private void SetMoneyEveryDay()
    {
        _cashBack += _balanceValue.Value * Percent;
    }

    private void IncreaseMoneyEveryMonth()
    {
        _balanceValue.IncreaseMoney(_cashBack);
    }

    private void SetTransactionLimit(decimal value)
    {
        TransactionLimit = value;
        Notifier.Notify("Transaction limit has been changed");
    }
}
using System.Collections.Immutable;
using System.Data.Common;
using System.Runtime.InteropServices;
using Banks.Abstractions;
using Banks.Models.DepositHandlers;

namespace Banks.Entities;

public delegate void NotifyAccountCreated(IBankAccount account);

public delegate void NotifyAccountPolicyHasBeenChanged(decimal value);

public delegate void NotifyAccountTimePolicyHasBeenChanged(TimeSpan value);

public class Bank
{
    private readonly List<IBankAccount> _accounts;
    private readonly List<Client> _clients;
    private decimal _creditLimit;
    private decimal _commission;
    private decimal _transactionLimit;
    private decimal _debitPercent;
    private TimeSpan _interval;

    private Bank(decimal debitPercent, decimal commission, decimal creditLimit, decimal transactionLimit, TimeSpan interval)
    {
        _debitPercent = debitPercent;
        _clients = new List<Client>();
        _accounts = new List<IBankAccount>();
        _commission = commission;
        _creditLimit = creditLimit;
        _transactionLimit = transactionLimit;
        _interval = interval;
        Id = Guid.NewGuid();
    }

    public event NotifyAccountCreated? OnAccountCreated;
    public event NotifyAccountPolicyHasBeenChanged? CommissionHasBeenChanged;
    public event NotifyAccountPolicyHasBeenChanged? CreditLimitHasBeenChanged;
    public event NotifyAccountPolicyHasBeenChanged? TransactionLimitHasBeenChanged;
    public event NotifyAccountTimePolicyHasBeenChanged? TimeIntervalHasBeenChanged;
    public event NotifyAccountPolicyHasBeenChanged? DebitPercentHasBeenChanged;
    public static BankBuilder Builder => new BankBuilder();
    public Guid Id { get; }

    public decimal CreditLimit
    {
        get => _creditLimit;
        set
        {
            _creditLimit = value;
            CreditLimitHasBeenChanged?.Invoke(value);
        }
    }

    public decimal Commission
    {
        get => _commission;
        set
        {
            _commission = value;
            CommissionHasBeenChanged?.Invoke(value);
        }
    }

    public decimal TransactionLimit
    {
        get => _transactionLimit;
        set
        {
            _transactionLimit = value;
            TransactionLimitHasBeenChanged?.Invoke(value);
        }
    }

    public decimal DebitPercent
    {
        get => _debitPercent;
        set
        {
            _debitPercent = value;
            DebitPercentHasBeenChanged?.Invoke(value);
        }
    }

    public TimeSpan Interval
    {
        get => _interval;
        set
        {
            _interval = value;
            TimeIntervalHasBeenChanged?.Invoke(value);
        }
    }

    public decimal CalculateDepositPercent(IDepositCalculator calculator, decimal value)
    {
        decimal? percent = calculator.HandleRequest(value);
        return (decimal)percent !;
    }

    public Guid CreateBankAccount(ICreateBankAccount creator)
    {
        IBankAccount account = creator.Build();
        _accounts.Add(account);
        OnAccountCreated?.Invoke(account);
        return account.Id;
    }

    public void RegisterClient(Client client)
    {
        _clients.Add(client);
    }

    public Client GetClientFromId(Guid id)
    {
        Client? client = _clients.FirstOrDefault(x => x.Id == id);
        if (client != null)
            return client !;
        throw new NullReferenceException();
    }

    public class BankBuilder
    {
        private decimal _debitPercent;
        private List<Client> _clients = new List<Client>();
        private List<IBankAccount> _accounts = new List<IBankAccount>();
        private decimal _commission;
        private decimal _creditLimit;
        private decimal _transactionLimit;
        private TimeSpan _interval;

        public BankBuilder WithDebitPercent(decimal debitPercent)
        {
            _debitPercent = debitPercent;
            return this;
        }

        public BankBuilder WithCommission(decimal commission)
        {
            _commission = commission;
            return this;
        }

        public BankBuilder WithCreditLimit(decimal creditLimit)
        {
            _creditLimit = creditLimit;
            return this;
        }

        public BankBuilder WithTransactionLimit(decimal transactionLimit)
        {
            _transactionLimit = transactionLimit;
            return this;
        }

        public BankBuilder WithInterval(TimeSpan interval)
        {
            _interval = interval;
            return this;
        }

        public Bank Build()
        {
            return new Bank(_debitPercent, _commission, _creditLimit, _transactionLimit, _interval);
        }
    }
}
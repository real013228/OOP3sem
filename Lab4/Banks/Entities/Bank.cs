using System.Collections.Immutable;
using System.Data.Common;
using System.Runtime.InteropServices;
using Banks.Abstractions;
using Banks.Models.DepositHandlers;

namespace Banks.Entities;

public delegate void NotifyAccountCreated(IBankAccount account);

public class Bank
{
    private readonly List<IBankAccount> _accounts;
    private List<Client> _clients;

    private Bank(decimal debitPercent, decimal commission, decimal creditLimit, decimal transactionLimit, TimeSpan interval)
    {
        DebitPercent = debitPercent;
        _clients = new List<Client>();
        _accounts = new List<IBankAccount>();
        Commission = commission;
        CreditLimit = creditLimit;
        TransactionLimit = transactionLimit;
        Interval = interval;
    }

    public event NotifyAccountCreated? OnAccountCreated;

    public static BankBuilder Builder => new BankBuilder();
    public decimal CreditLimit { get; set; }
    public decimal Commission { get; set; }
    public decimal TransactionLimit { get; set; }
    public decimal DebitPercent { get; }
    public TimeSpan Interval { get; }

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
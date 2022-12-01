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

    public Bank(decimal debitPercent, TimeSpan timeInterval, decimal commission, decimal creditLimit, decimal transactionLimit)
    {
        DebitPercent = debitPercent;
        _clients = new List<Client>();
        _accounts = new List<IBankAccount>();
        TimeInterval = timeInterval;
        Commission = commission;
        CreditLimit = creditLimit;
        TransactionLimit = transactionLimit;
    }

    public event NotifyAccountCreated? OnAccountCreated;

    public TimeSpan TimeInterval { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal Commission { get; set; }
    public decimal TransactionLimit { get; set; }
    public decimal DebitPercent { get; }

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
}
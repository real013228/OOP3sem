using System.Data.Common;
using System.Runtime.InteropServices;
using Banks.Abstractions;
using Banks.Models.DepositHandlers;

namespace Banks.Entities;

public delegate void NotifyAccountCreated(IBankAccount account);

public class Bank
{
    private readonly decimal _debitPercent;
    private List<Client> _clients;
    private List<IBankAccount> _accounts;

    public Bank(decimal debitPercent, TimeSpan timeInterval, decimal commission, decimal creditLimit, decimal transactionLimit)
    {
        _debitPercent = debitPercent;
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

    public decimal CalculateDepositPercent(IDepositCalculator calculator, decimal value)
    {
        decimal? percent = calculator.HandleRequest(value);
        return (decimal)percent !;
    }

    public IBankAccount CreateDebitAccount(Client client, decimal account, IClock clock)
    {
        var debitAccount = new DebitAccount(_debitPercent, account, client, clock);
        OnAccountCreated?.Invoke(debitAccount);
        return debitAccount;
    }

    public IBankAccount CreateDepositAccount(Client client, decimal account, IDepositCalculator calculator, IClock clock)
    {
        var depositAccount = new DepositAccount(CalculateDepositPercent(calculator, account), account, client, clock);
        OnAccountCreated?.Invoke(depositAccount);
        return depositAccount;
    }

    public IBankAccount CreateCreditAccount(Client client, decimal account, IClock clock)
    {
        var creditAccount = new CreditAccount(Commission, account, client, clock);
        OnAccountCreated?.Invoke(creditAccount);
        return creditAccount;
    }
}
using Banks.Abstractions;
using Banks.Entities.Account;
using Banks.Models.DepositHandlers;

namespace Banks.Entities.AccountCreators;

public class CreateDepositAccount : ICreateBankAccount
{
    private readonly IClock _clock;
    private readonly IDepositCalculator _calculator;
    private readonly INotifyStrategy _notifier;
    private Client? _client;
    private decimal? _account;
    private Bank? _bank;

    public CreateDepositAccount(IClock clock, IDepositCalculator calculator, INotifyStrategy notifier)
    {
        _clock = clock;
        _calculator = calculator;
        _notifier = notifier;
    }

    public void SetBank(Bank bank)
    {
        _bank = bank;
    }

    public void SetClient(Client client)
    {
        _client = client;
    }

    public void SetAccount(decimal account)
    {
        _account = account;
    }

    public IBankAccount Build()
    {
        return new DepositAccount(_bank !.CalculateDepositPercent(_calculator, (decimal)_account !), (decimal)_account, _client !, _clock, _bank.Interval, _notifier, _bank, _bank.TransactionLimit);
    }
}
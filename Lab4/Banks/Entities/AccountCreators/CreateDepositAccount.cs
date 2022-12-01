using Banks.Abstractions;
using Banks.Entities.Account;
using Banks.Models.DepositHandlers;

namespace Banks.Entities.AccountCreators;

public class CreateDepositAccount : ICreateBankAccount
{
    private readonly Client _client;
    private readonly decimal _account;
    private readonly IClock _clock;
    private readonly IDepositCalculator _calculator;
    private Bank? _bank;

    public CreateDepositAccount(Bank? bank, Client client, IClock clock, decimal account, IDepositCalculator calculator)
    {
        _bank = bank;
        _client = client;
        _clock = clock;
        _account = account;
        _calculator = calculator;
    }

    public void SetBank(Bank bank)
    {
        _bank = bank;
    }

    public IBankAccount Build()
    {
        return new DepositAccount(_bank !.CalculateDepositPercent(_calculator, _account), _account, _client, _clock);
    }
}
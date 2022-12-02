using Banks.Abstractions;
using Banks.Entities.Account;

namespace Banks.Entities.AccountCreators;

public class CreateDebitAccount : ICreateBankAccount
{
    private readonly decimal _account;
    private readonly Client _client;
    private readonly IClock _clock;
    private readonly INotifyStrategy _notifier;
    private Bank? _bank;

    public CreateDebitAccount(decimal account, Client client, IClock clock, INotifyStrategy notifier)
    {
        _account = account;
        _client = client;
        _clock = clock;
        _notifier = notifier;
    }

    public void SetBank(Bank bank)
    {
        _bank = bank;
    }

    public IBankAccount Build()
    {
        return new DebitAccount(_bank !.DebitPercent, _account, _client, _clock, _notifier, _bank.TransactionLimit, _bank);
    }
}
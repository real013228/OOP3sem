using Banks.Abstractions;
using Banks.Entities.Account;

namespace Banks.Entities.AccountCreators;

public class CreateDebitAccount : ICreateBankAccount
{
    private readonly IClock _clock;
    private readonly INotifyStrategy _notifier;
    private decimal? _account;
    private Client? _client;
    private Bank? _bank;

    public CreateDebitAccount(IClock clock, INotifyStrategy notifier)
    {
        _clock = clock;
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
        return new DebitAccount(_bank !.DebitPercent, (decimal)_account !, _client !, _clock, _notifier, _bank.TransactionLimit, _bank);
    }
}
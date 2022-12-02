using Banks.Abstractions;
using Banks.Entities.Account;

namespace Banks.Entities.AccountCreators;

public class CreateCreditAccount : ICreateBankAccount
{
    private readonly IClock _clock;
    private readonly INotifyStrategy _notifier;
    private Client? _client;
    private decimal? _account;
    private Bank? _bank;

    public CreateCreditAccount(IClock clock, INotifyStrategy notifier)
    {
        _clock = clock;
        _notifier = notifier;
    }

    public void SetClient(Client client)
    {
        _client = client;
    }

    public void SetAccount(decimal account)
    {
        _account = account;
    }

    public void SetBank(Bank bank)
    {
        _bank = bank;
    }

    public IBankAccount Build()
    {
        return new CreditAccount(_bank !.Commission, (decimal)_account !, _client !, _clock, _bank.CreditLimit, _bank, _bank.TransactionLimit, _notifier);
    }
}
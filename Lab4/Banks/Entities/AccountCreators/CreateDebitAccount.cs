using Banks.Abstractions;
using Banks.Entities.Account;

namespace Banks.Entities.AccountCreators;

public class CreateDebitAccount : ICreateBankAccount
{
    private Bank? _bank;
    private decimal _account;
    private Client _client;
    private IClock _clock;

    public CreateDebitAccount(decimal account, Client client, IClock clock)
    {
        _account = account;
        _client = client;
        _clock = clock;
    }

    public void SetBank(Bank bank)
    {
        _bank = bank;
    }

    public IBankAccount Build()
    {
        return new DebitAccount(_bank !.DebitPercent, _account, _client, _clock);
    }
}
﻿using Banks.Abstractions;
using Banks.Entities.Account;

namespace Banks.Entities.AccountCreators;

public class CreateCreditAccount : ICreateBankAccount
{
    private readonly Client _client;
    private readonly decimal _account;
    private readonly IClock _clock;
    private readonly INotifyStrategy _notifier;
    private Bank? _bank;

    public CreateCreditAccount(Client client, decimal account, IClock clock, INotifyStrategy notifier)
    {
        _client = client;
        _account = account;
        _clock = clock;
        _notifier = notifier;
    }

    public void SetBank(Bank bank)
    {
        _bank = bank;
    }

    public IBankAccount Build()
    {
        return new CreditAccount(_bank !.Commission, _account, _client, _clock, _bank.CreditLimit, _bank, _bank.TransactionLimit, _notifier);
    }
}
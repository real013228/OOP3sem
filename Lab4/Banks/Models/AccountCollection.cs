using System.ComponentModel;
using Banks.Abstractions;

namespace Banks.Entities;

public class AccountCollection
{
    private List<IBankAccount> _accounts;

    public AccountCollection(List<IBankAccount> accounts)
    {
        _accounts = accounts;
    }

    public bool Contains(IBankAccount account)
    {
        return _accounts.Contains(account);
    }
}
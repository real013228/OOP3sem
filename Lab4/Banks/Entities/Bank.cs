using System.Runtime.InteropServices;
using Banks.Abstractions;

namespace Banks.Entities;

public class Bank
{
    private List<Client> _clients;
    private List<IBankAccount> _accounts;

    public Bank()
    {
        _clients = new List<Client>();
        _accounts = new List<IBankAccount>();
    }

    public Client ClientRegistration(string firstName, string lastName, string passport, string address, List<IBankAccount> accounts)
    {
        bool isSus = !(passport == string.Empty || address == string.Empty);
        Client client = Client.Builder
            .WithFirstName(firstName)
            .WithLastName(lastName)
            .WithAddress(address)
            .WithPassport(passport)
            .WithAccounts(accounts)
            .IsSus(isSus)
            .Build();
        return client;
    }
}
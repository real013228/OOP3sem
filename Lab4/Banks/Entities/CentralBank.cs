using Banks.Abstractions;

namespace Banks.Entities;

public class CentralBank : ICentralBank
{
    private readonly List<Bank> _banks;
    private readonly List<Client> _clients;
    private readonly List<IBankAccount> _accounts;

    public CentralBank()
    {
        _accounts = new List<IBankAccount>();
        _clients = new List<Client>();
        _banks = new List<Bank>();
    }

    public Bank CreateBank(decimal debitPercent, TimeSpan timeInterval, decimal commission, decimal creditLimit, decimal transactionLimit)
    {
        var bank = new Bank(debitPercent, timeInterval, commission, creditLimit, transactionLimit);
        _banks.Add(bank);
        bank.OnAccountCreated += AccountCreated;
        return bank;
    }

    public Client RegisterClient(string firstName, string lastName)
    {
        Client client = Client.Builder
            .WithFirstName(firstName)
            .WithLastName(lastName)
            .Build();
        _clients.Add(client);
        return client;
    }

    private void AccountCreated(IBankAccount account)
    {
        _accounts.Add(account);
    }
}
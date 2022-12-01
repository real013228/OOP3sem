using Banks.Abstractions;

namespace Banks.Entities;

public class CentralBank : ICentralBank
{
    private readonly List<Bank> _banks;
    private readonly List<IBankAccount> _accounts;

    public CentralBank()
    {
        _accounts = new List<IBankAccount>();
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
        return client;
    }

    public void MakeTransaction(ITransaction transaction)
    {
        var visitor = new TransactionVisitor();
    }

    private void AccountCreated(IBankAccount account)
    {
        _accounts.Add(account);
    }
}
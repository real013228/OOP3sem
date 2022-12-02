using Banks.Abstractions;

namespace Banks.Entities;

public class CentralBank : ICentralBank
{
    private readonly List<Bank> _banks;
    private readonly List<IBankAccount> _accounts;
    private readonly List<TransactionWrapper> _transactions;
    private readonly List<Client> _clients;

    public CentralBank()
    {
        _accounts = new List<IBankAccount>();
        _banks = new List<Bank>();
        _transactions = new List<TransactionWrapper>();
        _clients = new List<Client>();
    }

    public Bank CreateBank(Bank.BankBuilder builder)
    {
        Bank bank = builder
            .Build();
        _banks.Add(bank);
        bank.OnAccountCreated += AccountCreated;
        return bank;
    }

    public Client RegisterClient(Client.ClientBuilder clientBuilder, Bank bank)
    {
        Client client = clientBuilder
            .Build();
        bank.RegisterClient(client);
        _clients.Add(client);
        return client;
    }

    public void MakeTransaction(ITransaction transaction)
    {
        var visitor = new TransactionVisitor(_accounts);
        transaction.Accept(visitor);
        _transactions.Add(visitor.Transaction !);
    }

    public void CancelTransaction(ITransaction transaction)
    {
        TransactionWrapper? transactionWrapper = _transactions.FirstOrDefault(x => x.Transaction.Id == transaction.Id);
        if (transactionWrapper == null)
            throw new NullReferenceException();
        transactionWrapper.CancelTransaction();
        _transactions.Remove(transactionWrapper);
    }

    public Bank GetBankFromId(Guid id)
    {
        Bank? bank = _banks.FirstOrDefault(x => x.Id == id);
        if (bank != null)
            return bank;
        throw new NullReferenceException();
    }

    private void AccountCreated(IBankAccount account)
    {
        _accounts.Add(account);
    }
}
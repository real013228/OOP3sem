using Banks.Abstractions;

namespace Banks.Entities;

public class CentralBank : ICentralBank
{
    private readonly List<Bank> _banks;
    private readonly List<IBankAccount> _accounts;
    private readonly List<TransactionWrapper> _transactions;

    public CentralBank()
    {
        _accounts = new List<IBankAccount>();
        _banks = new List<Bank>();
        _transactions = new List<TransactionWrapper>();
    }

    public Bank CreateBank(Bank.BankBuilder builder)
    {
        Bank bank = builder
            .Build();
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

    private void AccountCreated(IBankAccount account)
    {
        _accounts.Add(account);
    }
}
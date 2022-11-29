using Banks.Abstractions;

namespace Banks.Entities;

public class DepositAccount : IBankAccount
{
    public DepositAccount(decimal percent, decimal startAccount, Client clientAccount)
    {
        Percent = percent;
        Commission = 0;
        Account = startAccount;
        ClientAccount = clientAccount;
        Id = Guid.NewGuid();
    }

    public Client ClientAccount { get; }
    public decimal Percent { get; }
    public decimal Commission { get; }
    public decimal Account { get; private set; }
    public Guid Id { get; }
    public void TakeMoney(decimal value)
    {
        throw new NullReferenceException();
    }

    public void TopUpMoney(decimal value)
    {
        Account += value;
    }

    public void TransferMoney(IBankAccount otherClientAccount, decimal value)
    {
        if (Account < value)
            throw new NullReferenceException();
        Account -= value;
        otherClientAccount.TopUpMoney(value);
    }

    public void TopUpMoney(Client client, decimal value)
    {
        if (!client.Accounts.Contains(this))
            throw new NullReferenceException();
        Account += value;
    }
}
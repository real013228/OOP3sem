using Banks.Abstractions;

namespace Banks.Entities;

public class DebitAccount : IBankAccount
{
    public DebitAccount(decimal percent, decimal account, Client clientAccount)
    {
        Percent = percent;
        Commission = 0;
        Account = account;
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
        if (Account < value)
            throw new NullReferenceException();
        Account -= value;
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
}
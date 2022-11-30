using Banks.Abstractions;

namespace Banks.Entities;

public class DebitAccount : IBankAccount
{
    public DebitAccount(decimal percent, decimal account, Client clientAccount, IClock clock)
    {
        Percent = percent;
        Commission = 0;
        Account = account;
        ClientAccount = clientAccount;
        Clock = clock;
        Id = Guid.NewGuid();
    }

    public Client ClientAccount { get; }
    public decimal TransactionLimit { get; set; }
    public decimal Percent { get; }
    public decimal Commission { get; }
    public decimal Account { get; private set; }
    public Guid Id { get; }
    public IClock Clock { get; }

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
}
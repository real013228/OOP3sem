using Banks.Abstractions;

namespace Banks.Entities;

public class CreditAccount : IBankAccount
{
    public CreditAccount(decimal commission, decimal account, Client clientAccount, IClock clock)
    {
        Percent = 0;
        Commission = commission;
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

    // Надо перенести isSus Client => Account.
    // Публичные сеттеры для паспорта и адреса
    // Id для клиента и банка?
    // Подумать над отменой транзакции
    // 29.11.2022 21:12 -- устал.
    // todo ;
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
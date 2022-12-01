using Banks.Abstractions;
using Banks.Models;

namespace Banks.Entities.Account;

public class CreditAccount : IBankAccount
{
    public CreditAccount(decimal commission, decimal account, Client clientAccount, IClock clock)
    {
        Percent = 0;
        Commission = commission;
        BalanceValue = new Balance(account);
        ClientAccount = clientAccount;
        Clock = clock;
        Id = Guid.NewGuid();
    }

    public Client ClientAccount { get; }
    public decimal TransactionLimit { get; set; }
    public decimal Percent { get; }
    public decimal Commission { get; }
    public Balance BalanceValue { get; }

    // Надо перенести isSus Client => Account.
    // Публичные сеттеры для паспорта и адреса
    // Id для клиента и банка?
    // Подумать над отменой транзакции
    // 29.11.2022 21:12 -- устал.
    // Надо написать билдер для банка
    // 30.11.2022 21:44 -- чуть меньше устал
    // todo ;
    public Guid Id { get; }
    public IClock Clock { get; }

    public void TakeMoney(decimal value)
    {
        if (ClientAccount.IsSus && TransactionLimit < value)
            throw new NullReferenceException();
        BalanceValue.DecreaseMoney(value);
    }

    public void TopUpMoney(decimal value)
    {
        if (ClientAccount.IsSus && TransactionLimit < value)
            throw new NullReferenceException();
        BalanceValue.IncreaseMoney(value);
    }
}
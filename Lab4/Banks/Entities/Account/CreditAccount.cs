using Banks.Abstractions;
using Banks.Entities.AccountCreators;
using Banks.Models;

namespace Banks.Entities.Account;

public class CreditAccount : IBankAccount
{
    public CreditAccount(decimal commission, decimal account, Client clientAccount, IClock clock, decimal creditLimit)
    {
        Percent = 0;
        Commission = commission;
        BalanceValue = new Balance(account);
        ClientAccount = clientAccount;
        Clock = clock;
        CreditLimit = creditLimit;
        Id = Guid.NewGuid();
    }

    public Client ClientAccount { get; }
    public decimal CreditLimit { get; set; }
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

    public decimal TakeMoney(decimal value)
    {
        if (ClientAccount.IsSus && TransactionLimit < value)
            throw new NullReferenceException();
        return BalanceValue.Value < 0 ? BalanceValue.DecreaseMoney(value + Commission) : BalanceValue.DecreaseMoney(value);
    }

    public decimal TopUpMoney(decimal value)
    {
        if (BalanceValue.Value < 0)
        {
            if (ClientAccount.IsSus && TransactionLimit < value &&
                CreditLimit > BalanceValue.Value - value - Commission)
                throw new NullReferenceException();
            return BalanceValue.IncreaseMoney(value + Commission);
        }

        if (ClientAccount.IsSus && TransactionLimit < value && CreditLimit > BalanceValue.Value - value)
            throw new NullReferenceException();
        return BalanceValue.IncreaseMoney(value);
    }
}
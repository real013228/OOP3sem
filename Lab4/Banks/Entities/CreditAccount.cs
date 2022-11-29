using Banks.Abstractions;

namespace Banks.Entities;

public class CreditAccount : IBankAccount
{
    public CreditAccount(decimal commission, decimal account, Client clientAccount)
    {
        Percent = 0;
        Commission = commission;
        Account = account;
        ClientAccount = clientAccount;
        Id = Guid.NewGuid();
    }

    public Client ClientAccount { get; }
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

    public void TakeMoney(decimal value)
    {
        throw new NotImplementedException();
    }

    public void TopUpMoney(decimal value)
    {
        throw new NotImplementedException();
    }

    public void TransferMoney(IBankAccount otherClientAccount, decimal value)
    {
        if (Account < value)
            throw new NullReferenceException();
        Account -= value;
        otherClientAccount.TopUpMoney(value);
    }
}
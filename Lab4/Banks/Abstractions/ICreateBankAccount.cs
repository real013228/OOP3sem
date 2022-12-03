using Banks.Entities;

namespace Banks.Abstractions;

public interface ICreateBankAccount
{
    void SetBank(Bank bank);
    void SetAccount(decimal account);
    void SetClient(Client client);
    IBankAccount Build();
}
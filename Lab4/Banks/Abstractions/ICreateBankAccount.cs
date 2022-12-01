using Banks.Entities;

namespace Banks.Abstractions;

public interface ICreateBankAccount
{
    void SetBank(Bank bank);
    IBankAccount Build();
}
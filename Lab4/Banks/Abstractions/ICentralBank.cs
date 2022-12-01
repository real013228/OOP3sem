using Banks.Entities;

namespace Banks.Abstractions;

public interface ICentralBank
{
    Bank CreateBank(Bank.BankBuilder builder);
    Client RegisterClient(string firstName, string lastName);
    void MakeTransaction(ITransaction transaction);
}
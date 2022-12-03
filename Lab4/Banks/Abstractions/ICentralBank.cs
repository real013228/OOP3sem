using Banks.Entities;

namespace Banks.Abstractions;

public interface ICentralBank
{
    public Bank GetBankFromId(Guid id);
    Bank CreateBank(Bank.BankBuilder builder);
    Client RegisterClient(Client.ClientBuilder clientBuilder, Bank bank);
    void MakeTransaction(ITransaction transaction);
    void CancelTransaction(Guid transactionId);
}
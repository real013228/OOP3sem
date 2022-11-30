using Banks.Entities;

namespace Banks.Abstractions;

public interface ICentralBank
{
    Bank CreateBank(decimal debitPercent, TimeSpan timeInterval, decimal commission, decimal creditLimit, decimal transactionLimit);
    Client RegisterClient(string firstName, string lastName);
}
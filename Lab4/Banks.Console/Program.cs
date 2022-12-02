using Banks.Abstractions;
using Banks.Entities;
using Banks.Entities.Account;

namespace Banks.Console;

public static class Program
{
    public static void Main()
    {
        IClock clock = new Clock(10);
        Client client = Client.Builder
            .WithFirstName("Natsuki")
            .WithLastName("Subaru")
            .Build();
        var timeSpan = new TimeSpan(20000000);
        Bank bank = Bank
            .Builder
            .WithCommission(1)
            .WithInterval(timeSpan)
            .WithCreditLimit(1)
            .WithDebitPercent(1)
            .WithTransactionLimit(1)
            .Build();
        var notifier = new NotifyStrategy(client, System.Console.WriteLine);
        IBankAccount account = new DepositAccount(1, 1, client, clock, timeSpan, notifier, bank, bank.TransactionLimit);
        clock.SetArbitraryTime(timeSpan);
        clock.StartTimer();
    }
}
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
        IBankAccount account = new DepositAccount(1, 1, client, clock, timeSpan);
        clock.SetArbitraryTime(timeSpan);
        clock.StartTimer();
    }
}
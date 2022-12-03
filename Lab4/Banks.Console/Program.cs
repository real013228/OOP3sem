using System.ComponentModel;
using Banks.Abstractions;
using Banks.ConsoleApplicationHandlers;
using Banks.ConsoleApplicationHandlers.CancelTransaction;
using Banks.ConsoleApplicationHandlers.CreateBank;
using Banks.ConsoleApplicationHandlers.CreateBankAccount;
using Banks.ConsoleApplicationHandlers.CreateBankAccount.Credit;
using Banks.ConsoleApplicationHandlers.CreateUser;
using Banks.ConsoleApplicationHandlers.MakeTransaction;
using Banks.Entities;
using Banks.Entities.Account;
using Banks.Entities.AccountCreators;

namespace Banks.Console;

public static class Program
{
    public static void Main()
    {
        while (true)
        {
            var value = System.Console.ReadLine();
            if (int.TryParse(value, out int _))
                break;
        }

        System.Console.WriteLine("\nHey there! I am using WhatsApp. Do you want to create a central bank and a bank?\ny/n");
        while (System.Console.ReadKey().KeyChar != 'y')
        {
            System.Console.WriteLine("\nPlease enter \"y\"");
        }

        ICentralBank centralBank = new CentralBank();
        System.Console.WriteLine(" ");
        var bankCreator = new CreateBankHandler(centralBank);
        bankCreator.Handle('1');
        Bank bankForAll = bankCreator.Builder !.Build();

        while (true)
        {
            MenuLog();
            ConsoleKeyInfo key = System.Console.ReadKey();
            if (key.KeyChar == 'q')
            {
                System.Console.WriteLine("See you soon!");
                break;
            }

            var notifyStrategyAction = new Action<string>(System.Console.WriteLine);
            Client client = Client.Builder
                .WithFirstName("Natsuki")
                .WithLastName("Subaru")
                .Build();
            var notifyStrategy = new NotifyStrategy(client, notifyStrategyAction);
            var clock = new FrozenClock();
            var clientCreator = new CreateUserHandler(centralBank, bankForAll);
            var bankAccountCreator = new CreateBankAccountHandler(centralBank, bankForAll, notifyStrategy, clock);
            var makeTransactionCreator = new MakeTransactionHandler(centralBank);
            var cancelTransaction = new CancelTransactionHandler(centralBank);
            bankCreator.SetNextHandler(clientCreator);
            clientCreator.SetNextHandler(bankAccountCreator);
            bankAccountCreator.SetNextHandler(makeTransactionCreator);
            makeTransactionCreator.SetNextHandler(cancelTransaction);
            clientCreator.Handle(key.KeyChar);
        }
    }

    public static void Requiem()
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

    private static void MenuLog()
    {
        System.Console.WriteLine("2 - Create user");
        System.Console.WriteLine("3 - Create bank account");
        System.Console.WriteLine("4 - Make transaction");
        System.Console.WriteLine("5 - Cancel transaction");
        System.Console.WriteLine("q - Quit from menu");
    }
}
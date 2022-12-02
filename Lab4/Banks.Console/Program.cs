using System.ComponentModel;
using Banks.Abstractions;
using Banks.ConsoleApplicationHandlers;
using Banks.ConsoleApplicationHandlers.CancelTransaction;
using Banks.ConsoleApplicationHandlers.CreateBank;
using Banks.ConsoleApplicationHandlers.CreateBankAccount;
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

        System.Console.WriteLine("\nHey there! I am using WhatsApp. Do you want to create a central bank?\ny/n");
        while (System.Console.ReadKey().KeyChar != 'y')
        {
            System.Console.WriteLine("\nPlease enter \"y\"");
        }

        ICentralBank centralBank = new CentralBank();
        System.Console.WriteLine(" ");
        while (true)
        {
            MenuLog();
            ConsoleKeyInfo key = System.Console.ReadKey();
            if (key.KeyChar == 'q')
            {
                System.Console.WriteLine("See you soon!");
                break;
            }

            IConsoleApplicationHandler bankCreator = new CreateBankHandler(centralBank);
            IConsoleApplicationHandler clientCreator = new CreateUserHandler(centralBank);
            IConsoleApplicationHandler bankAccountCreator = new CreateBankAccountHandler(centralBank);
            IConsoleApplicationHandler makeTransactionCreator = new MakeTransactionHandler(centralBank);
            IConsoleApplicationHandler cancelTransaction = new CancelTransactionHandler(centralBank);
            bankCreator.SetNextHandler(clientCreator);
            clientCreator.SetNextHandler(bankAccountCreator);
            bankAccountCreator.SetNextHandler(makeTransactionCreator);
            makeTransactionCreator.SetNextHandler(cancelTransaction);

            var builder = SetBankConfigurationHandlers(bankCreator, centralBank);
            SetUserConfigurationHandlers(clientCreator, centralBank, centralBank.CreateBank(builder));
            SetBankAccountConfiguration(bankAccountCreator, centralBank, centralBank.CreateBank(builder));
            bankCreator.Handle(key.KeyChar);
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

    private static Bank.BankBuilder SetBankConfigurationHandlers(IConsoleApplicationHandler bankCreator, ICentralBank centralBank)
    {
        Bank.BankBuilder builder = Bank.Builder;
        var commissionHandler = new SetCommissionHandler(centralBank, builder);
        var creditLimitHandler = new SetCreditLimitHandler(centralBank, builder);
        var debitPercentHandler = new SetDebitPercentHandler(centralBank, builder);
        var transactionLimitHandler = new SetTransactionLimitHandler(centralBank, builder);
        var timeIntervalHandler = new SetTimeIntervalHandler(centralBank, builder);
        bankCreator.SetLessResponsibilitiesHandler(commissionHandler);
        commissionHandler.SetNextHandler(creditLimitHandler);
        creditLimitHandler.SetNextHandler(debitPercentHandler);
        debitPercentHandler.SetNextHandler(transactionLimitHandler);
        transactionLimitHandler.SetNextHandler(timeIntervalHandler);
        return builder;
    }

    private static void SetUserConfigurationHandlers(IConsoleApplicationHandler userCreator, ICentralBank centralBank, Bank bank)
    {
        Client.ClientBuilder builder = Client.Builder;
        var firstNameHandler = new SetFirstName(builder, centralBank, bank);
        var secondNameHandler = new SetSecondName(builder, centralBank, bank);
        userCreator.SetLessResponsibilitiesHandler(firstNameHandler);
        firstNameHandler.SetNextHandler(secondNameHandler);
    }

    private static void SetBankAccountConfiguration(IConsoleApplicationHandler accountCreator, ICentralBank centralBank, Bank bank)
    {
        Client client = Client.Builder
            .WithFirstName("kirusha")
            .WithLastName("savvinov")
            .Build();
        var builder = new CreateCreditAccount(new FrozenClock(), new NotifyStrategy(client, System.Console.WriteLine));
        var creditClientHandler = new SetCreditClientHandler(bank, builder);
        var creditAccountHandler = new SetCreditAccountHandler(bank, builder);
        creditClientHandler.SetNextHandler(creditAccountHandler);
        accountCreator.SetLessResponsibilitiesHandler(creditClientHandler);
    }

    private static void MenuLog()
    {
        System.Console.WriteLine("1 - Create bank");
        System.Console.WriteLine("2 - Create user");
        System.Console.WriteLine("3 - Create bank account");
        System.Console.WriteLine("4 - Make transaction");
        System.Console.WriteLine("5 - Cancel transaction");
        System.Console.WriteLine("q - Quit from menu");
    }
}
using System.Collections.Immutable;
using Banks.Abstractions;
using Banks.ConsoleApplicationHandlers.HandlerAbstractions;
using Banks.Entities;

namespace Banks.ConsoleApplicationHandlers.CreateBank;

public class CreateBankHandler : IConsoleApplicationHandler
{
    private IConsoleApplicationHandler? _nextHandler;
    private ISetBankParameter? _handler;

    public CreateBankHandler(ICentralBank centralBank)
    {
        MainCentralBank = centralBank;
    }

    public ICentralBank MainCentralBank { get; }
    public Bank.BankBuilder? Builder { get; private set; }

    public void SetLessResponsibilitiesHandler(IHandlerLessResponsibilities handler)
    {
        _handler = handler as ISetBankParameter;
    }

    public void SetNextHandler(IConsoleApplicationHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public void Handle(char key)
    {
        if (key == '1')
        {
            Console.WriteLine("\nDo you want to create a bank?\ny/n");
            if (Console.ReadKey().KeyChar != 'y') return;
            Bank.BankBuilder builder = Bank.Builder;
            Builder = builder;
            var commissionHandler = new SetCommissionHandler(MainCentralBank, builder);
            var creditLimitHandler = new SetCreditLimitHandler(MainCentralBank, builder);
            var debitPercentHandler = new SetDebitPercentHandler(MainCentralBank, builder);
            var transactionLimitHandler = new SetTransactionLimitHandler(MainCentralBank, builder);
            var timeIntervalHandler = new SetTimeIntervalHandler(MainCentralBank, builder);
            SetLessResponsibilitiesHandler(commissionHandler);
            commissionHandler.SetNextHandler(creditLimitHandler);
            creditLimitHandler.SetNextHandler(debitPercentHandler);
            debitPercentHandler.SetNextHandler(transactionLimitHandler);
            transactionLimitHandler.SetNextHandler(timeIntervalHandler);
            Console.WriteLine("\nPlease enter a commission for new bank");
            while (true)
            {
                string? commission = Console.ReadLine();
                if (commission != null && decimal.TryParse(commission, out decimal _))
                {
                    Console.WriteLine($"Commission has been set successfully! New value is {commission}");
                    _handler?.Handle(commission);
                    Console.WriteLine("The bank has been created successfully!");
                    break;
                }

                Console.WriteLine("Please try again");
            }
        }
        else
        {
            _nextHandler?.Handle(key);
        }
    }
}
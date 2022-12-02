using Banks.Abstractions;
using Banks.ConsoleApplicationHandlers.HandlerAbstractions;
using Banks.Entities;

namespace Banks.ConsoleApplicationHandlers.CreateBank;

public class SetTransactionLimitHandler : ISetBankParameter
{
    private ISetBankParameter? _nextHandler;

    public SetTransactionLimitHandler(ICentralBank mainCentralBank, Bank.BankBuilder builder)
    {
        MainCentralBank = mainCentralBank;
        Builder = builder;
    }

    public ICentralBank MainCentralBank { get; }

    public Bank.BankBuilder Builder { get; }

    public void SetNextHandler(ISetBankParameter nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public void Handle(string value)
    {
        Builder.WithTransactionLimit(decimal.Parse(value));
        while (true)
        {
            Console.WriteLine("Please set deposit account term for your new bank");
            string? timeInterval = Console.ReadLine();
            if (timeInterval != null && TimeSpan.TryParse(timeInterval, out TimeSpan outNum))
            {
                Console.WriteLine($"Deposit account term has been set successfully! New value is {timeInterval}");
                _nextHandler?.Handle(timeInterval);
                break;
            }

            Console.WriteLine("Try Again");
        }
    }
}
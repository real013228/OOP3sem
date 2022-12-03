using Banks.Abstractions;
using Banks.ConsoleApplicationHandlers.HandlerAbstractions;
using Banks.Entities;

namespace Banks.ConsoleApplicationHandlers.CreateBank;

public class SetDebitPercentHandler : ISetBankParameter
{
    private ISetBankParameter? _nextHandler;

    public SetDebitPercentHandler(ICentralBank mainCentralBank, Bank.BankBuilder builder)
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
        Builder.WithDebitPercent(decimal.Parse(value));
        while (true)
        {
            Console.WriteLine("Please set transaction limit for your new bank");
            string? transactionLimit = Console.ReadLine();
            if (transactionLimit != null && decimal.TryParse(transactionLimit, out decimal outNum))
            {
                Console.WriteLine($"Transaction limit has been set successfully! New value is {transactionLimit}");
                _nextHandler?.Handle(transactionLimit);
                break;
            }

            Console.WriteLine("Try Again");
        }
    }
}
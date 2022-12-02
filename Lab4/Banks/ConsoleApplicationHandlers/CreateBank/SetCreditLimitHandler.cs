using System.Collections.Immutable;
using Banks.Abstractions;
using Banks.ConsoleApplicationHandlers.HandlerAbstractions;
using Banks.Entities;

namespace Banks.ConsoleApplicationHandlers.CreateBank;

public class SetCreditLimitHandler : ISetBankParameter
{
    private ISetBankParameter? _nextHandler;

    public SetCreditLimitHandler(ICentralBank mainCentralBank, Bank.BankBuilder builder)
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
        Builder.WithCreditLimit(decimal.Parse(value));
        while (true)
        {
            Console.WriteLine("Please set debit percent for your new bank");
            string? debitPercent = Console.ReadLine();
            if (debitPercent != null && decimal.TryParse(debitPercent, out decimal outNum))
            {
                Console.WriteLine($"Debit percent has been set successfully! New value is {debitPercent}");
                _nextHandler?.Handle(debitPercent);
                break;
            }

            Console.WriteLine("Try Again");
        }
    }
}
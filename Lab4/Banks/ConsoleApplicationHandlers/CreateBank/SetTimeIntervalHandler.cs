using Banks.Abstractions;
using Banks.ConsoleApplicationHandlers.HandlerAbstractions;
using Banks.Entities;

namespace Banks.ConsoleApplicationHandlers.CreateBank;

public class SetTimeIntervalHandler : ISetBankParameter
{
    private ISetBankParameter? _nextHandler;

    public SetTimeIntervalHandler(ICentralBank mainCentralBank, Bank.BankBuilder builder)
    {
        MainCentralBank = mainCentralBank;
        Builder = builder;
    }

    public ICentralBank MainCentralBank { get; }

    public Bank.BankBuilder Builder { get; }
    public Bank? Bank { get; private set; }

    public void SetNextHandler(ISetBankParameter nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public void Handle(string value)
    {
        Builder.WithInterval(TimeSpan.Parse(value));
        Bank = MainCentralBank.CreateBank(Builder);
    }
}
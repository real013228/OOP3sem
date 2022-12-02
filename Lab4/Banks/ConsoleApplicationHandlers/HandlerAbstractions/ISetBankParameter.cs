using Banks.Abstractions;
using Banks.Entities;

namespace Banks.ConsoleApplicationHandlers.HandlerAbstractions;

public interface ISetBankParameter : IHandlerLessResponsibilities
{
    public ICentralBank MainCentralBank { get; }
    public Bank.BankBuilder Builder { get; }
    public void SetNextHandler(ISetBankParameter nextHandler);
}
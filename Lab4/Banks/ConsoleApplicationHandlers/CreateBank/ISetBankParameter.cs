using Banks.Entities;

namespace Banks.ConsoleApplicationHandlers.CreateBank;

public interface ISetBankParameter
{
    public Bank.BankBuilder Builder { get; }
    public void SetBackToMenu(ISetBankParameter menuHandler);
    public void SetNextHandler(ISetBankParameter nextHandler);
}
using Banks.Abstractions;

namespace Banks.ConsoleApplicationHandlers;

public interface IConsoleApplicationHandler
{
    public ICentralBank MainCentralBank { get; }
    void SetNextHandler(IConsoleApplicationHandler nextHandler);
    void SetBackToMenu(IConsoleApplicationHandler menuHandler);
    void Handle(char key);
}
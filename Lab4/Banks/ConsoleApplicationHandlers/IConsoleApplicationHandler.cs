using Banks.Abstractions;
using Banks.ConsoleApplicationHandlers.CreateBank;

namespace Banks.ConsoleApplicationHandlers;

public interface IConsoleApplicationHandler
{
    public ICentralBank MainCentralBank { get; }
    void SetLessResponsibilitiesHandler(IHandlerLessResponsibilities handler);
    void SetNextHandler(IConsoleApplicationHandler nextHandler);
    void Handle(char key);
}
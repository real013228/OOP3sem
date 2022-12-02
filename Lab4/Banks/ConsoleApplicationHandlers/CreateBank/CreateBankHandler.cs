using Banks.Abstractions;

namespace Banks.ConsoleApplicationHandlers.CreateBank;

public class CreateBankHandler : IConsoleApplicationHandler
{
    private IConsoleApplicationHandler? _nextHandler;
    private IConsoleApplicationHandler? _menuHandler;

    public CreateBankHandler(ICentralBank centralBank)
    {
        MainCentralBank = centralBank;
    }

    public ICentralBank MainCentralBank { get; }

    public void SetNextHandler(IConsoleApplicationHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public void SetBackToMenu(IConsoleApplicationHandler menuHandler)
    {
        _menuHandler = menuHandler;
    }

    public void Handle(char key)
    {
        if (key == 'y')
        {
            // MainCentralBank.CreateBank();
        }
    }
}
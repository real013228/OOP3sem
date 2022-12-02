using Banks.Abstractions;

namespace Banks.ConsoleApplicationHandlers.MakeTransaction;

public class MakeTransactionHandler : IConsoleApplicationHandler
{
    private IConsoleApplicationHandler? _nextHandler;
    public MakeTransactionHandler(ICentralBank mainCentralBank)
    {
        MainCentralBank = mainCentralBank;
    }

    public ICentralBank MainCentralBank { get; }
    public void SetLessResponsibilitiesHandler(IHandlerLessResponsibilities handler)
    {
        throw new NotImplementedException();
    }

    public void SetNextHandler(IConsoleApplicationHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public void Handle(char key)
    {
        if (key == '4')
        {
            Console.WriteLine("Coming soon!");
        }
        else
        {
            _nextHandler?.Handle(key);
        }
    }
}
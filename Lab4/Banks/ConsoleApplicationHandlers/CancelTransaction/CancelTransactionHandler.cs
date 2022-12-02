using Banks.Abstractions;

namespace Banks.ConsoleApplicationHandlers.CancelTransaction;

public class CancelTransactionHandler : IConsoleApplicationHandler
{
    public CancelTransactionHandler(ICentralBank mainCentralBank)
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
        throw new NotImplementedException();
    }

    public void Handle(char key)
    {
        if (key == '5')
        {
            Console.WriteLine("\nComing soon!");
        }
    }
}
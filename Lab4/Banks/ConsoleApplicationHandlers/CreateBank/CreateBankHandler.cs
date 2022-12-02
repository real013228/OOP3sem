using Banks.Abstractions;
using Banks.Entities;

namespace Banks.ConsoleApplicationHandlers.CreateBank;

public class CreateBankHandler : IConsoleApplicationHandler
{
    private IConsoleApplicationHandler? _nextHandler;
    private IConsoleApplicationHandler? _menuHandler;
    private List<IConsoleApplicationHandler> _builderHandlers;

    public CreateBankHandler(ICentralBank centralBank, List<IConsoleApplicationHandler> builderHandlers)
    {
        MainCentralBank = centralBank;
        _builderHandlers = builderHandlers;
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
        if (key == '2')
        {
            Console.WriteLine("Do you want to create a bank?\ny/n");
            if (Console.ReadKey().KeyChar == 'y')
            {
                Console.WriteLine("Please enter a commission for new bank");
                string? commission = Console.ReadLine();
                while (true)
                {
                    if (commission != null)
                        _nextHandler.Handle(commission !);
                    else
                        Console.WriteLine("Please try again");
                }
            }
        }
    }
}
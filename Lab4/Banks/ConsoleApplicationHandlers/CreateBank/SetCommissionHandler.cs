using Banks.Abstractions;

namespace Banks.ConsoleApplicationHandlers.CreateBank;

public class SetCommissionHandler : ISetBankParameter
{
    private IConsoleApplicationHandler? _nextHandler;
    private IConsoleApplicationHandler? _menuHandler;

    public SetCommissionHandler(ICentralBank mainCentralBank)
    {
        MainCentralBank = mainCentralBank;
    }

    public decimal? Commission { get; private set; }
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
        if (key == '1')
        {
            Console.Clear();
            Console.WriteLine("Please set commission for your new bank");
            string? commission = Console.ReadLine();
            if (commission != null && int.TryParse(commission, out int outNum))
            {
                Commission = outNum;
                Console.WriteLine($"Commission has been set successfully! New value is {Commission}");
            }
            else
            {
                Console.WriteLine("Try Again");
            }
        }
        else
        {
            _nextHandler?.Handle(key);
        }
    }
}
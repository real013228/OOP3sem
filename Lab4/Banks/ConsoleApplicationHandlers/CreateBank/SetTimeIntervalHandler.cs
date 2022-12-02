using Banks.Abstractions;

namespace Banks.ConsoleApplicationHandlers.CreateBank;

public class SetTimeIntervalHandler : ISetBankParameter
{
    private IConsoleApplicationHandler? _nextHandler;
    private IConsoleApplicationHandler? _menuHandler;

    public SetTimeIntervalHandler(ICentralBank mainCentralBank)
    {
        MainCentralBank = mainCentralBank;
    }

    public TimeSpan? Interval { get; private set; }
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
        if (key == '5')
        {
            Console.Clear();
            Console.WriteLine("Please set deposit account time interval for your new bank in days");
            string? timeInterval = Console.ReadLine();
            if (timeInterval != null && int.TryParse(timeInterval, out int outNum))
            {
                Interval = TimeSpan.FromDays(outNum);
                Console.WriteLine($"Deposit account time interval has been set successfully! New value is {Interval}");
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
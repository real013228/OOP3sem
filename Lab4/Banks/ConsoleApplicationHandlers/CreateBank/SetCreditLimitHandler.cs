using Banks.Abstractions;

namespace Banks.ConsoleApplicationHandlers.CreateBank;

public class SetCreditLimitHandler : ISetBankParameter
{
    private IConsoleApplicationHandler? _nextHandler;
    private IConsoleApplicationHandler? _menuHandler;

    public SetCreditLimitHandler(ICentralBank mainCentralBank)
    {
        MainCentralBank = mainCentralBank;
    }

    public ICentralBank MainCentralBank { get; }
    public decimal? CreditLimit { get; private set; }

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
            Console.Clear();
            Console.WriteLine("Please set credit limit for your new bank");
            string? creditLimit = Console.ReadLine();
            if (creditLimit != null && int.TryParse(creditLimit, out int outNum))
            {
                CreditLimit = outNum;
                Console.WriteLine($"Credit limit has been set successfully! New value is {CreditLimit}");
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
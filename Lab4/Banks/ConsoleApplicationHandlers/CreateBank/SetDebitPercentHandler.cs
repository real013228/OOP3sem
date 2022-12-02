using Banks.Abstractions;

namespace Banks.ConsoleApplicationHandlers.CreateBank;

public class SetDebitPercentHandler : ISetBankParameter
{
    private IConsoleApplicationHandler? _nextHandler;
    private IConsoleApplicationHandler? _menuHandler;

    public SetDebitPercentHandler(ICentralBank mainCentralBank)
    {
        MainCentralBank = mainCentralBank;
    }

    public decimal? DebitPercent { get; private set; }
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
        if (key == '3')
        {
            Console.Clear();
            Console.WriteLine("Please set debit percent for your new bank");
            string? debitPercent = Console.ReadLine();
            if (debitPercent != null && int.TryParse(debitPercent, out int outNum))
            {
                DebitPercent = outNum;
                Console.WriteLine($"Debit percent has been set successfully! New value is {DebitPercent}");
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
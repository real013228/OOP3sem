using Banks.Abstractions;

namespace Banks.ConsoleApplicationHandlers.CreateBank;

public class SetTransactionLimitHandler : ISetBankParameter
{
    private IConsoleApplicationHandler? _nextHandler;
    private IConsoleApplicationHandler? _menuHandler;

    public SetTransactionLimitHandler(ICentralBank mainCentralBank)
    {
        MainCentralBank = mainCentralBank;
    }

    public decimal? TransactionLimit { get; private set; }
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
        if (key == '4')
        {
            Console.Clear();
            Console.WriteLine("Please set transaction limit for your new bank");
            string? transactionLimit = Console.ReadLine();
            if (transactionLimit != null && int.TryParse(transactionLimit, out int outNum))
            {
                TransactionLimit = outNum;
                Console.WriteLine($"Transaction limitr has been set successfully! New value is {TransactionLimit}");
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
using Banks.Abstractions;
using Banks.Entities;

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
            Console.WriteLine("Do you want to make a transaction?\ny/n");
            if (Console.ReadKey().KeyChar != 'y') return;
            Console.WriteLine("\nChoose a number as a kind of transaction you want to make");
            Console.WriteLine("1 - Increase money");
            Console.WriteLine("2 - Decrease money");
            Console.WriteLine("3 - Transfer money");
            ConsoleKeyInfo line = Console.ReadKey();
            switch (line.KeyChar)
            {
                case '1':
                    Console.WriteLine("Nice choose in awesome bank, bro");
                    break;
                case '2':
                    Console.WriteLine("Good transaction for great client, bro");
                    break;
                case '3':
                    Console.WriteLine("Generosity is a well quality, bro");
                    break;
                default:
                    Console.WriteLine("Normalno vyberi ee russkim yazykov zhe napisano 1 2 3 vybiray");
                    break;
            }
        }
        else
        {
            _nextHandler?.Handle(key);
        }
    }
}
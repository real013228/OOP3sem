using Banks.Abstractions;
using Banks.ConsoleApplicationHandlers.HandlerAbstractions;

namespace Banks.ConsoleApplicationHandlers.CreateBankAccount;

public class CreateBankAccountHandler : IConsoleApplicationHandler
{
    private IConsoleApplicationHandler? _nextHandler;
    private ISetBankAccountParameter? _creditHandler;
    private ISetBankAccountParameter? _debitHandler;
    private ISetBankAccountParameter? _depositHandler;

    public CreateBankAccountHandler(ICentralBank mainCentralBank)
    {
        MainCentralBank = mainCentralBank;
    }

    public ICentralBank MainCentralBank { get; }

    public void SetLessResponsibilitiesHandler(IHandlerLessResponsibilities handler)
    {
        _creditHandler = handler as ISetBankAccountParameter;
    }

    public void SetLessResponsibilitiesHandlerDebit(IHandlerLessResponsibilities handler)
    {
        _debitHandler = handler as ISetBankAccountParameter;
    }

    public void SetLessResponsibilitiesHandlerDeposit(IHandlerLessResponsibilities handler)
    {
        _depositHandler = handler as ISetBankAccountParameter;
    }

    public void SetNextHandler(IConsoleApplicationHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public void Handle(char key)
    {
        if (key == '3')
        {
            Console.WriteLine("\nDo you want to create a bank account?\ny/n");
            if (Console.ReadKey().KeyChar != 'y') return;
            Console.WriteLine("\nChoose a number as a kind of account you want to create");
            Console.WriteLine("1 - Credit account");
            Console.WriteLine("2 - Debit account");
            Console.WriteLine("3 - Deposit account");
            while (true)
            {
                ConsoleKeyInfo newKey = Console.ReadKey();
                switch (newKey.KeyChar)
                {
                    case '1':
                        Console.WriteLine("\nPlease set a client name");
                        string? client = Console.ReadLine();
                        if (client != null)
                        {
                            Console.WriteLine($"Client has been set successfully!");
                            _creditHandler?.Handle(client);
                            Console.WriteLine("The credit account has been created successfully!");
                        }

                        break;
                    case '2':
                        Console.WriteLine("\nComing soon!");
                        break;
                    case '3':
                        Console.WriteLine("\nComing soon!");
                        break;
                    default:
                        Console.WriteLine("Please try again");
                        break;
                }

                Console.WriteLine("Please try again");
            }
        }
        else
        {
            _nextHandler?.Handle(key);
        }
    }
}
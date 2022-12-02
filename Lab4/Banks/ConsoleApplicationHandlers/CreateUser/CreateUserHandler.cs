using Banks.Abstractions;
using Banks.ConsoleApplicationHandlers.HandlerAbstractions;

namespace Banks.ConsoleApplicationHandlers.CreateUser;

public class CreateUserHandler : IConsoleApplicationHandler
{
    private IConsoleApplicationHandler? _nextHandler;
    private ISetUserHandler? _handler;
    public CreateUserHandler(ICentralBank mainCentralBank)
    {
        MainCentralBank = mainCentralBank;
    }

    public ICentralBank MainCentralBank { get; }
    public void SetLessResponsibilitiesHandler(IHandlerLessResponsibilities handler)
    {
        _handler = handler as ISetUserHandler;
    }

    public void SetNextHandler(IConsoleApplicationHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public void Handle(char key)
    {
        if (key == '2')
        {
            Console.WriteLine("\nDo you want to make registration as a client?\ny/n");
            if (Console.ReadKey().KeyChar != 'y') return;
            Console.WriteLine("\nPlease enter your first name");
            while (true)
            {
                string? firstName = Console.ReadLine();
                if (firstName != null)
                {
                    Console.WriteLine($"Your first name has been set successfully! New value is {firstName}");
                    _handler?.Handle(firstName);
                    Console.WriteLine("You have passed a registration successfully!");
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
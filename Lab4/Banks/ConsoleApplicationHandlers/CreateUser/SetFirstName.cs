using Banks.Abstractions;
using Banks.ConsoleApplicationHandlers.HandlerAbstractions;
using Banks.Entities;

namespace Banks.ConsoleApplicationHandlers.CreateUser;

public class SetFirstName : ISetUserHandler
{
    private ISetUserHandler? _nextHandler;

    public SetFirstName(Client.ClientBuilder builder, ICentralBank centralBank, Bank bank)
    {
        Builder = builder;
        CentralBank = centralBank;
        Bank = bank;
    }

    public ICentralBank CentralBank { get; }
    public Bank Bank { get; }
    public Client.ClientBuilder Builder { get; }

    public void SetNextHandler(ISetUserHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public void Handle(string value)
    {
        Builder.WithFirstName(value);
        while (true)
        {
            Console.WriteLine("Please set your second name");
            string? secondName = Console.ReadLine();
            if (secondName != null)
            {
                Console.WriteLine($"Second name has been set successfully! New value is {secondName}");
                _nextHandler?.Handle(secondName);
                break;
            }

            Console.WriteLine("Try Again");
        }
    }
}
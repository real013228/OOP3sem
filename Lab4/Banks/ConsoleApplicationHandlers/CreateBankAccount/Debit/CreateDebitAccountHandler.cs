using Banks.Abstractions;
using Banks.ConsoleApplicationHandlers.HandlerAbstractions;
using Banks.Entities;
using Banks.Entities.AccountCreators;

namespace Banks.ConsoleApplicationHandlers.CreateBankAccount.Debit;

public class CreateDebitAccountHandler : IConsoleApplicationHandler
{
    private IConsoleApplicationHandler? _nextHandler;
    private ISetBankAccountParameter? _handler;
    private Bank _bank;
    private IClock _clock;
    private INotifyStrategy _strategy;

    public CreateDebitAccountHandler(ICentralBank mainCentralBank, Bank bank, INotifyStrategy strategy, IClock clock)
    {
        MainCentralBank = mainCentralBank;
        _bank = bank;
        _strategy = strategy;
        _clock = clock;
    }

    public ICentralBank MainCentralBank { get; }

    public void SetLessResponsibilitiesHandler(IHandlerLessResponsibilities handler)
    {
        _handler = handler as ISetBankAccountParameter;
    }

    public void SetNextHandler(IConsoleApplicationHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public void Handle(char key)
    {
        if (key == '2')
        {
            var creator = new CreateDebitAccount(_clock, _strategy);
            var clientHandler = new SetClientHandler(_bank, creator);
            var accountHandler = new SetAccountHandler(_bank, creator);
            clientHandler.SetNextHandler(accountHandler);
            SetLessResponsibilitiesHandler(clientHandler);
            while (true)
            {
                Console.WriteLine("Please set a client Id");
                string? clientName = Console.ReadLine();
                if (clientName != null && Guid.TryParse(clientName, out Guid _))
                {
                    Console.WriteLine($"User name has been set successfully! Value is - {clientName}");
                    _handler?.Handle(clientName);
                    Console.WriteLine($"Debit account has been created successfully!");
                    break;
                }

                Console.WriteLine("Try again at next time");
                break;
            }
        }
        else
        {
            _nextHandler?.Handle(key);
        }
    }
}
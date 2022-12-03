using Banks.Abstractions;
using Banks.ConsoleApplicationHandlers.HandlerAbstractions;
using Banks.Entities;
using Banks.Entities.AccountCreators;
using Banks.Models.DepositHandlers;

namespace Banks.ConsoleApplicationHandlers.CreateBankAccount.Deposit;

public class CreateDepositAccountHandler : IConsoleApplicationHandler
{
    private IConsoleApplicationHandler? _nextHandler;
    private ISetBankAccountParameter? _handler;
    private Bank _bank;
    private IClock _clock;
    private INotifyStrategy _strategy;

    public CreateDepositAccountHandler(ICentralBank mainCentralBank, Bank bank, INotifyStrategy strategy, IClock clock)
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
        if (key == '3')
        {
            var depositCalculator1 = new ConcreteDepositHandler1();
            var depositCalculator2 = new ConcreteDepositHandler2();
            var depositCalculator3 = new ConcreteDepositHandler3();
            depositCalculator1.NextHandler = depositCalculator2;
            depositCalculator2.NextHandler = depositCalculator3;
            var creator = new CreateDepositAccount(_clock, depositCalculator1, _strategy);
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
                    Console.WriteLine($"Deposit account has been created successfully!");
                    break;
                }

                Console.WriteLine("Try again at next time");
                break;
            }
        }
    }
}
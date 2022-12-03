using Banks.Abstractions;
using Banks.ConsoleApplicationHandlers.CreateBankAccount.Credit;
using Banks.ConsoleApplicationHandlers.CreateBankAccount.Debit;
using Banks.ConsoleApplicationHandlers.HandlerAbstractions;
using Banks.Entities;

namespace Banks.ConsoleApplicationHandlers.CreateBankAccount;

public class CreateBankAccountHandler : IConsoleApplicationHandler
{
    private readonly Bank _bank;
    private readonly IClock _clock;
    private readonly INotifyStrategy _strategy;
    private IConsoleApplicationHandler? _nextHandler;
    private ISetBankAccountParameter? _handler;

    public CreateBankAccountHandler(ICentralBank mainCentralBank, Bank bank, INotifyStrategy strategy, IClock clock)
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
            Console.WriteLine("\nDo you want to create a bank account?\ny/n");
            if (Console.ReadKey().KeyChar != 'y') return;
            Console.WriteLine("\nChoose a number as a kind of account you want to create");
            Console.WriteLine("1 - Credit account");
            Console.WriteLine("2 - Debit account");
            Console.WriteLine("3 - Deposit account");
            var creditAccount = new CreateCreditAccountHandler(MainCentralBank, _bank, _clock, _strategy);
            var debitAccount = new CreateDebitAccountHandler(MainCentralBank, _bank, _strategy, _clock);
            var depositAccount = new CreateCreditAccountHandler(MainCentralBank, _bank, _clock, _strategy);
            creditAccount.SetNextHandler(debitAccount);
            debitAccount.SetNextHandler(depositAccount);
            while (true)
            {
                ConsoleKeyInfo newKey = Console.ReadKey();
                if (newKey.KeyChar is '1' or '2' or '3')
                {
                    creditAccount.Handle(newKey.KeyChar);
                }
                else
                {
                    Console.WriteLine("\nChoose a number as a kind of account you want to create");
                    Console.WriteLine("1 - Credit account");
                    Console.WriteLine("2 - Debit account");
                    Console.WriteLine("3 - Deposit account");
                }
            }
        }
        else
        {
            _nextHandler?.Handle(key);
        }
    }
}
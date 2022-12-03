using Banks.Abstractions;
using Banks.ConsoleApplicationHandlers.HandlerAbstractions;
using Banks.Entities;
using Banks.Entities.AccountCreators;

namespace Banks.ConsoleApplicationHandlers.CreateBankAccount;

public class SetClientHandler : ISetBankAccountParameter
{
    private ISetBankAccountParameter? _nextHandler;

    public SetClientHandler(Bank bank, ICreateBankAccount createBankAccount)
    {
        Bank = bank;
        CreateBankAccount = createBankAccount;
    }

    public Bank Bank { get; }
    public ICreateBankAccount CreateBankAccount { get; }

    public void SetNextHandler(ISetBankAccountParameter nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public void Handle(string value)
    {
        Console.WriteLine("\nPlease set the start money to your credit account");
        CreateBankAccount.SetClient(Bank.GetClientFromId(new Guid(value)));
        while (true)
        {
            string? accountValue = Console.ReadLine();
            if (accountValue != null && decimal.TryParse(accountValue, out decimal _))
            {
                Console.WriteLine("Start account has been set successfully");
                _nextHandler?.Handle(accountValue);
                break;
            }

            Console.WriteLine("Try Again");
        }
    }
}
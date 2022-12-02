using System.Collections.Immutable;
using Banks.Abstractions;
using Banks.ConsoleApplicationHandlers.HandlerAbstractions;
using Banks.Entities;
using Banks.Entities.Account;
using Banks.Entities.AccountCreators;

namespace Banks.ConsoleApplicationHandlers.CreateBankAccount;

public class SetCreditClientHandler : ISetBankAccountParameter
{
    private ISetBankAccountParameter? _nextHandler;

    public SetCreditClientHandler(Bank bank, CreateCreditAccount createBankAccount)
    {
        Bank = bank;
        CreateBankAccount = createBankAccount;
    }

    public Bank Bank { get; }
    public CreateCreditAccount CreateBankAccount { get; }

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
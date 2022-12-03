using Banks.Abstractions;
using Banks.ConsoleApplicationHandlers.HandlerAbstractions;
using Banks.Entities;
using Banks.Entities.AccountCreators;

namespace Banks.ConsoleApplicationHandlers.CreateBankAccount;

public class SetAccountHandler : ISetBankAccountParameter
{
    private ISetBankAccountParameter? _nextHandler;

    public SetAccountHandler(Bank bank, ICreateBankAccount createBankAccount)
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
        CreateBankAccount.SetAccount(decimal.Parse(value));
        Bank.CreateBankAccount(CreateBankAccount);
    }
}
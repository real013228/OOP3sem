using Banks.ConsoleApplicationHandlers.HandlerAbstractions;
using Banks.Entities;
using Banks.Entities.AccountCreators;

namespace Banks.ConsoleApplicationHandlers.CreateBankAccount;

public class SetCreditAccountHandler : ISetBankAccountParameter
{
    private ISetBankAccountParameter? _nextHandler;

    public SetCreditAccountHandler(Bank bank, CreateCreditAccount createBankAccount)
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
        CreateBankAccount.SetAccount(decimal.Parse(value));
        Bank.CreateBankAccount(CreateBankAccount);
    }
}
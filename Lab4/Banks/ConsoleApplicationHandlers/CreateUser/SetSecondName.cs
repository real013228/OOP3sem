using Banks.Abstractions;
using Banks.ConsoleApplicationHandlers.HandlerAbstractions;
using Banks.Entities;

namespace Banks.ConsoleApplicationHandlers.CreateUser;

public class SetSecondName : ISetUserHandler
{
    private ISetUserHandler? _nextHandler;

    public SetSecondName(Client.ClientBuilder builder, ICentralBank centralBank, Bank bank)
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
        Builder.WithLastName(value);
        CentralBank.RegisterClient(Builder, Bank);
    }
}
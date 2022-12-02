using Banks.Abstractions;
using Banks.Entities;

namespace Banks.ConsoleApplicationHandlers.HandlerAbstractions;

public interface ISetUserHandler : IHandlerLessResponsibilities
{
    ICentralBank CentralBank { get; }
    Bank Bank { get; }
    Client.ClientBuilder Builder { get; }
    void SetNextHandler(ISetUserHandler nextHandler);
}
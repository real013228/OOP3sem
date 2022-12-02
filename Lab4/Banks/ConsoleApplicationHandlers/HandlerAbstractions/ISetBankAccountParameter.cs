using Banks.Abstractions;
using Banks.Entities;

namespace Banks.ConsoleApplicationHandlers.HandlerAbstractions;

public interface ISetBankAccountParameter : IHandlerLessResponsibilities
{
    public Bank Bank { get; }

    public void SetNextHandler(ISetBankAccountParameter nextHandler);
}
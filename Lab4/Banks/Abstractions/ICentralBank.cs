using Banks.Entities;

namespace Banks.Abstractions;

public interface ICentralBank
{
    Bank CreateBank();
}
using Banks.Abstractions;

namespace Banks.Entities;

public class CentralBank : ICentralBank
{
    private readonly List<Bank> _banks;

    public CentralBank()
    {
        _banks = new List<Bank>();
    }

    public Bank CreateBank()
    {
        var bank = new Bank();
        _banks.Add(bank);
        return bank;
    }
}
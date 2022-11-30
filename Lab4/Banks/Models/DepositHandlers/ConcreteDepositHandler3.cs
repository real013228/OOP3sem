using Banks.Abstractions;

namespace Banks.Models.DepositHandlers;

public class ConcreteDepositHandler3 : IDepositCalculator
{
    private const decimal ConcretePercent = 4;
    private const decimal BottomLine = 50000;
    private const decimal UpperBound = 100000;
    public IDepositCalculator? NextHandler { get; set; }
    public decimal? HandleRequest(decimal value)
    {
        return value > BottomLine ? ConcretePercent : NextHandler?.HandleRequest(value);
    }
}
using Banks.Abstractions;

namespace Banks.Models.DepositHandlers;

public class ConcreteDepositHandler2 : IDepositCalculator
{
    private const decimal ConcretePercent = 3.5m;
    private const decimal BottomLine = 50000;
    private const decimal UpperBound = 100000;
    public IDepositCalculator? NextHandler { get; set; }
    public decimal? HandleRequest(decimal value)
    {
        return value is > BottomLine and < UpperBound ? ConcretePercent : NextHandler?.HandleRequest(value);
    }
}
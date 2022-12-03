using Banks.Abstractions;

namespace Banks.Models.DepositHandlers;

public class ConcreteDepositHandler1 : IDepositCalculator
{
    private const decimal ConcretePercent = 3;
    private const decimal BottomLine = 0;
    private const decimal UpperBound = 50000;
    public IDepositCalculator? NextHandler { get; set; }
    public decimal? HandleRequest(decimal value)
    {
        return value is > BottomLine and < UpperBound ? ConcretePercent : NextHandler?.HandleRequest(value);
    }
}
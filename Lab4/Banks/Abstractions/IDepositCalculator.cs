namespace Banks.Abstractions;

public interface IDepositCalculator
{
    IDepositCalculator? NextHandler { get; set; }
    decimal? HandleRequest(decimal value);
}
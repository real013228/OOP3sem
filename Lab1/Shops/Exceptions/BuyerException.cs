namespace Shops.Exceptions;

public class BuyerException : Exception
{
    private BuyerException(string msg)
        : base(msg) { }
    public static BuyerException NotEnoughMoneyException(decimal money)
    {
        return new BuyerException($"Invalid request: {money.ToString()} is not enough to buy");
    }
}
using Shops.Models;

namespace Shops.Exceptions;

public class SupplyException : Exception
{
    private SupplyException(string msg)
        : base(msg) { }

    public static SupplyException NotEnoughProducts()
    {
        return new SupplyException($"Invalid request: there is no enough products");
    }
}
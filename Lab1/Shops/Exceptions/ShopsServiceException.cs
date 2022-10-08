using Shops.Entities;

namespace Shops.Exceptions;

public class ShopsServiceException : Exception
{
    private ShopsServiceException(string msg)
        : base(msg) { }

    public static ShopsServiceException InvalidShopException(Shop shop)
    {
        return new ShopsServiceException($"Invalid request: There is not shop {shop.ShopName}");
    }
}
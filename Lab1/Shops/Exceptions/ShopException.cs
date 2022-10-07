using Shops.Entities;
using Shops.Models;

namespace Shops.Exceptions;

public class ShopException : Exception
{
    private ShopException(string msg)
        : base(msg) { }

    public static ShopException InvalidCountException(Product product)
    {
        return new ShopException($"Invalid request: there is no enough count {product.Name}");
    }
}
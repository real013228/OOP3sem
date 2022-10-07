using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Exceptions;

public class ProductPriceListException : Exception
{
    private ProductPriceListException(string msg)
        : base(msg) { }

    public static ProductPriceListException InvalidProductException(Product product)
    {
        return new ProductPriceListException($"Invalid request: there is no product {product.Name}");
    }
}
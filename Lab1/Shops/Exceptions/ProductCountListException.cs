using Shops.Entities;
using Shops.Models;

namespace Shops.Exceptions;

public class ProductCountListException : Exception
{
    private ProductCountListException(string msg)
        : base(msg) { }

    public static ProductCountListException InvalidProductException(Product product)
    {
        return new ProductCountListException($"Invalid request: there is no product {product.Name}");
    }
}
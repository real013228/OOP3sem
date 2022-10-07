using Shops.Models;

namespace Shops.Entities;

public class Store
{
    public Store()
    {
        List = new ProductCountList();
    }

    public ProductCountList List { get; }

    public int GetProductCount(Product product)
    {
        return List.GetProductCount(product);
    }
}
using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Models;

public class Supply
{
    public ProductCountList ProductSupply(Store store, ProductCountList list)
    {
        AddProducts(store, list);
        return list;
    }

    private ProductCountList AddProducts(Store store, ProductCountList list)
    {
        foreach (var product in list.List)
        {
            store.List.AddProduct(product.Key, product.Value);
        }

        return store.List;
    }
}
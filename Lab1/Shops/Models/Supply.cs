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
        list.List.ToList().ForEach(x => store.List.AddProduct(x.Key, x.Value));
        return store.List;
    }
}
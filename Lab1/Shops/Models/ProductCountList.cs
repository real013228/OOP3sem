using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Models;

public class ProductCountList
{
    public ProductCountList()
    {
        List = new Dictionary<Product, int>();
    }

    public ProductCountList(Dictionary<Product, int> list)
    {
        List = list;
    }

    public Dictionary<Product, int> List { get; }

    public int GetProductCount(Product product)
    {
        if (!List.ContainsKey(product))
        {
            throw ProductCountListException.InvalidProductException(product);
        }

        return List[product];
    }

    public void AddProductList(ProductCountList list)
    {
        foreach (KeyValuePair<Product, int> product in list.List)
        {
            AddProduct(product.Key, product.Value);
        }
    }

    public void AddProduct(Product product, int count)
    {
        if (List.ContainsKey(product))
        {
            List[product] += count;
        }
        else
        {
            List.Add(product, count);
        }
    }
}
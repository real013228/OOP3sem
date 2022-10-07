using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Models;

public class ProductPriceList
{
    public ProductPriceList()
    {
        TotalCost = 0;
        PriceList = new Dictionary<Product, decimal>();
    }

    public decimal TotalCost { get; private set; }
    public Dictionary<Product, decimal> PriceList { get; }

    public void AddProductList(ProductPriceList list)
    {
        foreach (var product in list.PriceList)
        {
            AddProduct(product.Key, product.Value);
        }
    }

    public void AddProduct(Product product, decimal price)
    {
        TotalCost += price;
        PriceList.Add(product, price);
    }

    public decimal GetProductPrice(Product product)
    {
        if (!PriceList.ContainsKey(product))
        {
            throw ProductPriceListException.InvalidProductException(product);
        }

        return PriceList[product];
    }
}
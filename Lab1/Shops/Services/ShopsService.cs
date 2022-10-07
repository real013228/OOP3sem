using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;

namespace Shops.Services;

public class ShopsService : IShopsService
{
    private readonly List<Shop> _shops;

    public ShopsService()
    {
        _shops = new List<Shop>();
    }

    public void AddShop(Shop shop)
    {
        _shops.Add(shop);
    }

    public ProductCountList SupplyProducts(Shop shop, ProductCountList list)
    {
        if (!_shops.Contains(shop))
        {
            throw ShopsServiceException.InvalidShopException(shop);
        }

        shop.RequestSupply(list);
        return list;
    }

    public decimal SetPrice(Shop shop, Product product, decimal newPrice)
    {
        if (!_shops.Contains(shop))
        {
            throw ShopsServiceException.InvalidShopException(shop);
        }

        decimal oldPrice = shop.GetProductInfo(product, 1);
        shop.ChangePrice(product, newPrice);
        return newPrice;
    }

    public Shop FindCheapestShop(Product product, int count)
    {
        decimal minPrice = 99999;
        foreach (var shop1 in _shops)
        {
            if (shop1.GetProductAvailability(product, count))
            {
                minPrice = Math.Min(shop1.GetProductInfo(product, count), minPrice);
            }
        }

        Shop? shop = _shops.FirstOrDefault(x => x.GetProductAvailability(product, count));
        if (shop == null)
        {
            throw new NullReferenceException();
        }

        return shop;
    }

    public ProductCountList PurchaseProductConsignment(Shop shop, Buyer person, ProductCountList list)
    {
        shop.MakePurchase(person, list);
        return list;
    }
}
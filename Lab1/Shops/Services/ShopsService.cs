using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;

namespace Shops.Services;

public class ShopsService : IShopsService
{
    private readonly Shop _shop1 =
        new Shop("Krusty Krab", "bikini bottom 23");

    private readonly Shop _shop2 = new Shop("Chum Bucket", "bikini bottoml 24");

    private readonly List<Shop> _shops;

    public ShopsService()
    {
        _shops = new List<Shop>
            { _shop1, _shop2 };
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
        decimal minPrice = _shops.Min(x => x.GetProductInfo(product, count));
        Shop? shop = _shops.FirstOrDefault(x => x.GetProductInfo(product, count) == minPrice);
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
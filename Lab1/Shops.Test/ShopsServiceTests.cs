using Shops.Entities;
using Shops.Models;
using Xunit;

namespace Shops.Test;

public class ShopsServiceTests
{
    [Fact]
    public void SupplyProducts_ShopHasProductsAndPersonCanBuyIt()
    {
        var person = new Buyer(1000);
        var shop = new Shop("Krusty Krab", "rossia");
        var product = new Product("milk");
        decimal price = 2;
        int cnt = 2;
        var productList = new ProductCountList(new Dictionary<Product, int>() { { product, cnt } });
        shop.RequestSupply(productList);
        Assert.Equal(shop.GetProductInfo(product, cnt), price);
        shop.MakePurchase(person, productList);
        Assert.Contains(product, person.Basket.List.Keys);
        Assert.Equal(998, person.Money);
    }
}
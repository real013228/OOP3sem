using System.Reflection.Metadata.Ecma335;
using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class ShopsServiceTests
{
    private readonly ShopsService _shopsService = new ShopsService();

    [Fact]
    public void SupplyProducts_ShopHasProductsAndPersonCanBuyIt()
    {
        var person = new Buyer(1000);
        var product = new Product("milk");
        decimal price = 2;
        int cnt = 2;
        var productList = new ProductCountList(new Dictionary<Product, int>() { { product, cnt } });
        var shop = new Shop("Krusty Krab", "bikini bottom 23");
        _shopsService.AddShop(shop);
        _shopsService.SupplyProducts(shop, productList);
        Assert.Equal(shop.GetProductInfo(product, cnt), price);
        _shopsService.PurchaseProductConsignment(shop, person, productList);
        Assert.Contains(product, person.Basket.List.Keys);
        Assert.Equal(998, person.Money);
    }

    [Fact]
    public void SetAndChangePrice_PriceChanged()
    {
        var shop = new Shop("Krusty Krab", "bikini bottom 23");
        _shopsService.AddShop(shop);
        var product = new Product("chocolate bar");
        decimal price = 2;
        _shopsService.SupplyProducts(shop, new ProductCountList(new Dictionary<Product, int>() { { product, 1 } }));
        _shopsService.SetPrice(shop, product, price);
        Assert.Equal(price, shop.GetProductInfo(product, 1));
    }

    [Fact]
    public void FindShopWhereProductIsCheapest()
    {
        var product1 = new Product("bread");
        var shop1 = new Shop("Krusty Krab1", "bikini bottom 23");
        var shop2 = new Shop("Krusty Krab2", "bikini bottom 23");
        var shop3 = new Shop("Krusty Krab3", "bikini bottom 23");
        _shopsService.AddShop(shop1);
        _shopsService.AddShop(shop2);
        _shopsService.AddShop(shop3);
        _shopsService.SupplyProducts(shop1, new ProductCountList(new Dictionary<Product, int>() { { product1, 1 } }));
        _shopsService.SupplyProducts(shop2, new ProductCountList(new Dictionary<Product, int>() { { product1, 2 } }));
        _shopsService.SupplyProducts(shop3, new ProductCountList(new Dictionary<Product, int>() { { product1, 3 } }));
        Assert.Equal(shop2, _shopsService.FindCheapestShop(product1, 2));

        var product2 = new Product("butter");
        Assert.Throws<ProductCountListException>(() => _shopsService.FindCheapestShop(product2, 1));
    }

    [Fact]
    public void PurchaseProductConsignment_BuyerHasNotEnoughMoney()
    {
        var shop = new Shop("Krusty Krab1", "bikini bottom 23");
        var person = new Buyer(1);
        var product = new Product("sniggers");
        var productList = new ProductCountList(new Dictionary<Product, int>() { { product, 10 } });
        _shopsService.AddShop(shop);
        _shopsService.SupplyProducts(shop, productList);
        Assert.Throws<BuyerException>(() => _shopsService.PurchaseProductConsignment(shop, person, productList));
    }
}
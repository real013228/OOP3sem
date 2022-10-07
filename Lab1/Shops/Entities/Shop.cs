using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Shop : IEquatable<Shop>
{
    private readonly Guid _id;
    private readonly Store _shopStore;
    private readonly Supply _provider;
    private string _address;

    public Shop(string shopName, string address)
    {
        _shopStore = new Store();
        ShopName = shopName;
        _id = new Guid(address);
        _address = address;
        _provider = new Supply();
        PriceList = new ProductPriceList();
    }

    public string ShopName { get; }
    public ProductPriceList PriceList { get; private set; }

    public ProductCountList RequestSupply(ProductCountList list)
    {
        return _provider.ProductSupply(_shopStore, list);
    }

    public decimal ChangePrice(Product product, decimal newCost)
    {
        decimal oldPrice = PriceList.GetProductPrice(product);
        PriceList.PriceList[product] = newCost;
        return newCost;
    }

    public decimal MakePurchase(Buyer person, ProductPriceList list)
    {
        person.Buy(list.TotalCost);
        person.Basket.AddProductList(list);
        return list.TotalCost;
    }

    public decimal GetProductInfo(Product product, int count)
    {
        int productCount = _shopStore.List.GetProductCount(product);
        if (_shopStore.List.GetProductCount(product) < count)
        {
            return PriceList.PriceList[product];
        }
        else
        {
            throw ShopException.InvalidCountException(product);
        }
    }

    public bool Equals(Shop? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || _id.Equals(other._id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((Shop)obj);
    }

    public override int GetHashCode()
    {
        return _id.GetHashCode();
    }

    private void SetPrice(ProductPriceList list)
    {
        PriceList = list;
    }
}
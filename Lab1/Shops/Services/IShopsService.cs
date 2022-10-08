using Shops.Entities;
using Shops.Models;

namespace Shops.Services;

public interface IShopsService
{
    public ProductCountList SupplyProducts(Shop shop, ProductCountList list);
    public decimal SetPrice(Shop shop, Product product, decimal newPrice);
    public Shop FindCheapestShop(Product product, int count);
    public ProductCountList PurchaseProductConsignment(Shop shop, Buyer person, ProductCountList list);
}
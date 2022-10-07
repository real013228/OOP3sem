using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Buyer
{
    public Buyer(decimal money)
    {
        Money = money;
        Basket = new ProductCountList();
    }

    public decimal Money { get; private set; }
    public ProductCountList Basket { get; }

    public void Buy(decimal cost, ProductCountList list)
    {
        if (Money >= cost)
        {
            Money -= cost;
            Basket.AddProductList(list);
        }
        else
        {
            throw BuyerException.NotEnoughMoneyException(Money);
        }
    }
}
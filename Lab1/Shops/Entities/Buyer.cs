using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Buyer
{
    public Buyer(decimal money)
    {
        Money = money;
        Basket = new ProductPriceList();
    }

    public decimal Money { get; private set; }
    public ProductPriceList Basket { get; }

    public void Buy(decimal cost)
    {
        if (Money >= cost)
        {
            Money -= cost;
        }
        else
        {
            throw BuyerException.NotEnoughMoneyException(Money);
        }
    }
}
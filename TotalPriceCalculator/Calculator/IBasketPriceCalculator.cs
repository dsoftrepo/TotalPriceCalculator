using System.Collections.Generic;
using TotalPriceCalculator.Basket;

namespace TotalPriceCalculator.Calculator
{
    public interface IBasketPriceCalculator
    {
        decimal CalculateTotal(IEnumerable<BasketItem> items);
        void UpdateItemLastPrice(BasketItem item);
    }
}

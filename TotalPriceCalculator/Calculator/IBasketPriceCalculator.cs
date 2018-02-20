using System.Collections.Generic;
using TotalPriceCalculator.Basket;

namespace TotalPriceCalculator.Calculator
{
    public interface IBasketPriceCalculator
    {
        decimal CalculateTotal(IEnumerable<IDiscountable> items);
        void ApplyDiscount(IDiscountable item);
    }
}

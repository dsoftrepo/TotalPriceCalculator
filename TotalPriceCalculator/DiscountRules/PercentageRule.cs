using System;
using TotalPriceCalculator.Basket;

namespace TotalPriceCalculator.DiscountRules
{
    public class PercentageRule : IDiscountRule
    {
        public void ApplyDiscount(BasketItem item, int parameter)
        {
            if (item == null) throw new ArgumentNullException("item");
            if (item.Product.DiscountRule != DiscountRuleType.Percentage) throw new InvalidOperationException("Invalid Discount rule applied");

            var fullPrice = item.Quantity * item.Product.Price;
            item.LastPrice = Math.Round(fullPrice - fullPrice * parameter/100, 2);
        }
    }
}

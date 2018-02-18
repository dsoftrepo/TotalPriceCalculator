using System;
using TotalPriceCalculator.Basket;

namespace TotalPriceCalculator.DiscountRules
{
    public class DefaultRule : IDiscountRule
    {
        public void ApplyDiscount(BasketItem item, int parameter)
        {
            if (item == null) throw new ArgumentNullException("item");
            if (item.Product.DiscountRule != DiscountRuleType.None) throw new InvalidOperationException("Invalid Discount rule applied");

            item.LastPrice = item.Quantity * item.Product.Price;
        }
    }
}

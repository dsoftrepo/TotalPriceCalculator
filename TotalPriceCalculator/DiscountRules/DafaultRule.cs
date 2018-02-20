using System;
using TotalPriceCalculator.Basket;

namespace TotalPriceCalculator.DiscountRules
{
    public class DefaultRule : IDiscountRule
    {
        public void ApplyDiscount(IDiscountable item)
        {
            if (item == null) throw new ArgumentNullException("item");
            if (item.DiscountRuleType != DiscountRuleType.None) throw new InvalidOperationException("Invalid Discount rule applied");

            item.AfterDiscountPrice = item.Quantity * item.UnitPrice;
        }
    }
}

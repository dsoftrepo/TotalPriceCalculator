using System;
using TotalPriceCalculator.Basket;

namespace TotalPriceCalculator.DiscountRules
{
    public class PercentageRule : IDiscountRule
    {
        public void ApplyDiscount(IDiscountable item)
        {
            if (item == null) throw new ArgumentNullException("item");
            if (item.DiscountRuleType != DiscountRuleType.Percentage) throw new InvalidOperationException("Invalid Discount rule applied");

            var fullPrice = item.Quantity * item.UnitPrice;
            item.AfterDiscountPrice = Math.Round(fullPrice - fullPrice * item.DiscountParameter/100, 2);
        }
    }
}

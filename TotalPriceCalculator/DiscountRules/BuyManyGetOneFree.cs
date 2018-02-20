using System;
using TotalPriceCalculator.Basket;

namespace TotalPriceCalculator.DiscountRules
{
    public class BuyManyGetOneFree : IDiscountRule
    {
        public void ApplyDiscount(IDiscountable item)
        {
            if (item == null) throw new ArgumentNullException("item");
            if (item.DiscountParameter < 2) throw new InvalidOperationException("Invalid parameter");
            if (item.DiscountRuleType != DiscountRuleType.BuyManyGetOneFree) throw new InvalidOperationException("Invalid Discount rule applied");

            if (item.Quantity == 1)
            {
                item.AfterDiscountPrice = item.UnitPrice;
                return;
            }

            var anySingles =  item.Quantity % item.DiscountParameter;
            item.AfterDiscountPrice = anySingles == 0
                ? item.UnitPrice * item.Quantity * (item.DiscountParameter - 1) / item.DiscountParameter
                : item.UnitPrice * ((item.Quantity - anySingles) * (item.DiscountParameter - 1) / item.DiscountParameter + anySingles);

            item.AfterDiscountPrice = Math.Round(item.AfterDiscountPrice, 2);
        }
    }
}

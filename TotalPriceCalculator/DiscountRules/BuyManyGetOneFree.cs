using System;
using TotalPriceCalculator.Basket;

namespace TotalPriceCalculator.DiscountRules
{
    public class BuyManyGetOneFree : IDiscountRule
    {
        public void ApplyDiscount(BasketItem item, int parameter)
        {
            if (item == null) throw new ArgumentNullException("item");
            if (parameter < 2) throw new InvalidOperationException("Invalid parameter");
            if (item.Product.DiscountRule != DiscountRuleType.BuyManyGetOneFree) throw new InvalidOperationException("Invalid Discount rule applied");

            if (item.Quantity == 1)
            {
                item.LastPrice = item.Product.Price;
                return;
            }

            var anySingles =  item.Quantity % parameter;
            item.LastPrice = anySingles == 0
                ? item.Product.Price * item.Quantity * (parameter - 1) / parameter
                : item.Product.Price * ((item.Quantity - anySingles) * (parameter - 1) / parameter + anySingles);

            item.LastPrice = Math.Round(item.LastPrice, 2);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using TotalPriceCalculator.Basket;
using TotalPriceCalculator.DiscountRules;

namespace TotalPriceCalculator.Calculator
{
    public class BasketPriceCalculator : IBasketPriceCalculator
    {
        private readonly IDiscountRuleFactory _discountRuleFactory;

        public BasketPriceCalculator(IDiscountRuleFactory discountRuleFactory)
        {
            _discountRuleFactory = discountRuleFactory;
        }

        public decimal CalculateTotal(IEnumerable<BasketItem> items)
        {
            return items.Sum(x => x.LastPrice);
        }

        public void UpdateItemLastPrice(BasketItem item)
        {
            if (!item.Product.DiscountRule.HasValue)
            {
                item.LastPrice = item.Quantity * item.Product.Price;
                return;
            }

            var discountRule = _discountRuleFactory.ResolveDiscountRule(item.Product.DiscountRule.Value);
            discountRule.ApplyDiscount(item, item.Product.DiscountParameter);
        }
    }
}

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

        public decimal CalculateTotal(IEnumerable<IDiscountable> items)
        {
            return items.Sum(x => x.AfterDiscountPrice);
        }

        public void ApplyDiscount(IDiscountable item)
        {
            if (!item.DiscountRuleType.HasValue)
            {
                item.AfterDiscountPrice = item.UnitPrice * item.Quantity;
                return;
            }

            var discountRule = _discountRuleFactory.ResolveDiscountRule(item.DiscountRuleType.Value);
            discountRule.ApplyDiscount(item);
        }
    }
}

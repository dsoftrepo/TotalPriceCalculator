using System;

namespace TotalPriceCalculator.DiscountRules
{
    public class DiscountRuleFactory : IDiscountRuleFactory
    {
        public IDiscountRule ResolveDiscountRule(DiscountRuleType type)
        {
            switch (type)
            {
                case DiscountRuleType.BuyManyGetOneFree: return new BuyManyGetOneFree();
                case DiscountRuleType.Percentage: return new PercentageRule();
                case DiscountRuleType.None: return new DefaultRule();
                default: throw new ArgumentException("Unhandeled discount type provided");
            }
        }
    }
}

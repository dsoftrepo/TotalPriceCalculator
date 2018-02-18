namespace TotalPriceCalculator.DiscountRules
{
    public interface IDiscountRuleFactory
    {
        IDiscountRule ResolveDiscountRule(DiscountRuleType type);
    }
}

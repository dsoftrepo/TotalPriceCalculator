using TotalPriceCalculator.DiscountRules;

namespace TotalPriceCalculator.Basket
{
    public interface IDiscountable
    {
        decimal UnitPrice { get;}
        int Quantity { get; set; }
        DiscountRuleType? DiscountRuleType { get; set; }
        int DiscountParameter { get; set; }
        decimal AfterDiscountPrice { get; set; }
    }
}

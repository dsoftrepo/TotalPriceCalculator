using TotalPriceCalculator.DiscountRules;

namespace TotalPriceCalculator.Basket
{
    public class Product
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DiscountRuleType? DiscountRule { get; set; }
        public int DiscountParameter { get; set; }
    }
}

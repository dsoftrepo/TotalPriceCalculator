using TotalPriceCalculator.DiscountRules;

namespace TotalPriceCalculator.Basket
{
    public class BasketItem : IDiscountable
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public decimal UnitPrice {
            get { return Product.Price; }
        }
        public int Quantity { get; set; }
        public DiscountRuleType? DiscountRuleType { get; set; }
        public int DiscountParameter { get; set; }
        public decimal AfterDiscountPrice { get; set; }
    }
}

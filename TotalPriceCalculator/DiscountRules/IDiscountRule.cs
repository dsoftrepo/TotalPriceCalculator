using TotalPriceCalculator.Basket;

namespace TotalPriceCalculator.DiscountRules
{
    public interface IDiscountRule
    {
        void ApplyDiscount(BasketItem item, int parameter);
    }
}

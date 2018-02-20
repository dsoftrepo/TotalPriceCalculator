using System;
using NUnit.Framework;
using TotalPriceCalculator.Basket;
using TotalPriceCalculator.DiscountRules;

namespace BasketPricesCalculator.Tests.DiscountRulesTests
{
    [TestFixture]
    public class PercentageTests
    {
        [Test]
        [TestCase(1, 10, 12.33, 11.1)]
        [TestCase(2, 10, 12.33, 22.19)]
        [TestCase(33, 10, 12.33, 366.2)]
        [TestCase(1, 20, 12.33, 9.86)]
        [TestCase(2, 20, 12.33, 19.73)]
        [TestCase(33, 20, 12.33, 325.51)]
        public void GivenProduct_WhenApplyDiscount_ThenReturnsCorrectPrice(int qty, int percent, decimal price, decimal result)
        {
            var discount = new PercentageRule();
            var item = new BasketItem
            {
                Quantity = qty,
                AfterDiscountPrice = 0,
                DiscountRuleType = DiscountRuleType.Percentage,
                DiscountParameter = percent,
                Product = new Product
                {
                    Price = price
                }
            };

            discount.ApplyDiscount(item);

            Assert.AreEqual(result, item.AfterDiscountPrice);
        }

        [Test]
        public void GivenNulProduct_WhenApplyDiscount_ThenThrowwsException()
        {
            var discount = new PercentageRule();
            Assert.Throws<ArgumentNullException>(() =>
            {
                discount.ApplyDiscount(null);
            });
        }

        [Test]
        public void GivenProductHasNotMatchingDiscountRule_WhenApplyDiscount_ThenThrowwsException()
        {
            var discount = new PercentageRule();
            var item = new BasketItem
            {
                Quantity = 2,
                AfterDiscountPrice = 0,
                DiscountRuleType = DiscountRuleType.None,
                Product = new Product
                {
                    Price = (decimal)12.33
                }
            };
            Assert.Throws<InvalidOperationException>(() =>
            {
                discount.ApplyDiscount(item);
            });
        }
    }
}

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
                LastPrice = 0,
                Product = new Product
                {
                    DiscountRule = DiscountRuleType.Percentage,
                    Price = price
                }
            };

            discount.ApplyDiscount(item, percent);

            Assert.AreEqual(result, item.LastPrice);
        }

        [Test]
        public void GivenNulProduct_WhenApplyDiscount_ThenThrowwsException()
        {
            var discount = new PercentageRule();
            Assert.Throws<ArgumentNullException>(() =>
            {
                discount.ApplyDiscount(null, 0);
            });
        }

        [Test]
        public void GivenProductHasNotMatchingDiscountRule_WhenApplyDiscount_ThenThrowwsException()
        {
            var discount = new PercentageRule();
            var item = new BasketItem
            {
                Quantity = 2,
                LastPrice = 0,
                Product = new Product
                {
                    DiscountRule = DiscountRuleType.None,
                    Price = (decimal)12.33
                }
            };
            Assert.Throws<InvalidOperationException>(() =>
            {
                discount.ApplyDiscount(item, 0);
            });
        }
    }
}

using System;
using NUnit.Framework;
using TotalPriceCalculator.Basket;
using TotalPriceCalculator.DiscountRules;

namespace BasketPricesCalculator.Tests.DiscountRulesTests
{
    [TestFixture]
    public class BuyManyGetOneFreeTests
    {
        [Test]
        [TestCase(1, 2, 12.33, 12.33)]
        [TestCase(2, 2, 12.33, 12.33)]
        [TestCase(3, 2, 12.33, 24.66)]
        [TestCase(4, 2, 12.33, 24.66)]
        [TestCase(32, 2, 12.33, 197.28)]
        [TestCase(33, 2, 12.33, 209.61)]
        public void GivenProduct_WhenApplyDiscount_ThenReturnsCorrectPrice(int qty, int minimumToBuy, decimal price, decimal result)
        {
            var discount = new BuyManyGetOneFree();
            var item = new BasketItem
            {
                Quantity = qty,
                AfterDiscountPrice = 0,
                DiscountRuleType = DiscountRuleType.BuyManyGetOneFree,
                DiscountParameter = minimumToBuy,
                Product = new Product
                {
                    Price = price
                }
            };

            discount.ApplyDiscount(item);

            Assert.AreEqual(result, item.AfterDiscountPrice);
        }

        [Test]
        [TestCase(1,3, 12.33, 12.33)]
        [TestCase(2,3, 12.33, 24.66)]
        [TestCase(3,3, 12.33, 24.66)]
        [TestCase(4,3, 12.33, 36.99)]
        [TestCase(5,3, 12.33, 49.32)]
        [TestCase(6,3, 12.33, 49.32)]
        [TestCase(7,3, 12.33, 61.65)]
        [TestCase(8,3, 12.33, 73.98)]
        [TestCase(9,3, 12.33, 73.98)]
        [TestCase(32,3, 12.33, 271.26)]
        [TestCase(33,3, 12.33, 271.26)]
        public void GivenProduct_WhenApplyDiscountBuy2Get1Free_ThenReturnsCorrectPrice(int qty, int minimumToBuy, decimal price, decimal result)
        {
            var discount = new BuyManyGetOneFree();
            var item = new BasketItem
            {
                Quantity = qty,
                AfterDiscountPrice = 0,
                DiscountRuleType = DiscountRuleType.BuyManyGetOneFree,
                DiscountParameter = minimumToBuy,
                Product = new Product
                {
                    
                    Price = price
                }
            };

            discount.ApplyDiscount(item);

            Assert.AreEqual(result, item.AfterDiscountPrice);
        }

        [Test]
        [TestCase(1,4, 12.33, 12.33)]
        [TestCase(2,4, 12.33, 24.66)]
        [TestCase(3,4, 12.33, 36.99)]
        [TestCase(4,4, 12.33, 36.99)]
        [TestCase(5,4, 12.33, 49.32)]
        [TestCase(6,4, 12.33, 61.65)]
        [TestCase(7,4, 12.33, 73.98)]
        [TestCase(8,4, 12.33, 73.98)]

        [TestCase(32,4, 12.33, 295.92)]
        [TestCase(33,4, 12.33, 308.25)]
        public void GivenProduct_WhenApplyDiscountBuy4Get1Free_ThenReturnsCorrectPrice(int qty, int minimumToBuy, decimal price, decimal result)
        {
            var discount = new BuyManyGetOneFree();
            var item = new BasketItem
            {
                Quantity = qty,
                AfterDiscountPrice = 0,
                DiscountRuleType = DiscountRuleType.BuyManyGetOneFree,
                DiscountParameter = minimumToBuy,
                Product = new Product
                {
                    
                    Price = price
                }
            };

            discount.ApplyDiscount(item);

            Assert.AreEqual(result, item.AfterDiscountPrice);
        }

        [Test]
        public void GivenNullProduct_WhenApplyDiscount_ThenThrowsException()
        {
            var discount = new BuyManyGetOneFree();
            Assert.Throws<ArgumentNullException>(() =>
            {
                discount.ApplyDiscount(null);
            });
        }

        [Test]
        public void GivenWrongParameter_WhenApplyDiscount_ThenThrowsException()
        {
            var discount = new BuyManyGetOneFree();
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

        [Test]
        public void GivenProductHasNotMatchingDiscountRule_WhenApplyDiscount_ThenThrowsException()
        {
            var discount = new BuyManyGetOneFree();
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

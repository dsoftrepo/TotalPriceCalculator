using System.Linq;
using NUnit.Framework;
using TotalPriceCalculator.Basket;
using TotalPriceCalculator.Calculator;
using TotalPriceCalculator.DiscountRules;

namespace BasketPricesCalculator.Tests.CalculatorTests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void GivenBasketItems_WhenCalculateTotal_ThenReturnsCorrectTotal()
        {
            var discountFactory = new DiscountRuleFactory();
            var calculator = new BasketPriceCalculator(discountFactory);

            var item = new BasketItem
            {
                Quantity = 2,
                AfterDiscountPrice = (decimal)10.50,
                DiscountRuleType = DiscountRuleType.BuyManyGetOneFree,
                DiscountParameter = 2,
                Product = new Product
                {
                    Price = (decimal)10.50
                }
            };

            var items = Enumerable.Repeat(item, 3);
            var total = calculator.CalculateTotal(items);

            Assert.AreEqual(31.50, total);
        }

        [Test]
        public void GivenBasketItem_Buy1Get1Free_WhenApplyDiscount_ThenItemIsUpdatedCorrectly()
        {
            var discountFactory = new DiscountRuleFactory();
            var calculator = new BasketPriceCalculator(discountFactory);

            var item = new BasketItem
            {
                Quantity = 2,
                AfterDiscountPrice = (decimal)10.50,
                DiscountRuleType = DiscountRuleType.BuyManyGetOneFree,
                DiscountParameter = 2,
                Product = new Product
                {
                    Price = (decimal)10.50
                }
            };

            calculator.ApplyDiscount(item);

            Assert.AreEqual(10.50, item.AfterDiscountPrice);
        }

        [Test]
        public void GivenBasketItem_Buy2Get1Free_WhenApplyDiscount_ThenItemIsUpdatedCorrectly()
        {
            var discountFactory = new DiscountRuleFactory();
            var calculator = new BasketPriceCalculator(discountFactory);

            var item = new BasketItem
            {
                Quantity = 3,
                AfterDiscountPrice = (decimal)10.50,
                DiscountRuleType = DiscountRuleType.BuyManyGetOneFree,
                DiscountParameter = 3,
                Product = new Product
                {
                    Price = (decimal)10.50
                }
            };

            calculator.ApplyDiscount(item);

            Assert.AreEqual(21, item.AfterDiscountPrice);
        }

        [Test]
        public void GivenBasketItem_Percentage_WhenApplyDiscount_ThenItemIsUpdatedCorrectly()
        {
            var discountFactory = new DiscountRuleFactory();
            var calculator = new BasketPriceCalculator(discountFactory);

            var item = new BasketItem
            {
                Quantity = 2,
                AfterDiscountPrice = 0,
                DiscountParameter = 10,
                DiscountRuleType = DiscountRuleType.Percentage,
                Product = new Product
                {
                    Price = (decimal) 20.10
                }
            };

            calculator.ApplyDiscount(item);

            Assert.AreEqual(36.18, item.AfterDiscountPrice);
        }

        [Test]
        public void GivenBasketItem_NoRule_Percentage_WhenApplyDiscount_ThenItemIsUpdatedCorrectly()
        {
            var discountFactory = new DiscountRuleFactory();
            var calculator = new BasketPriceCalculator(discountFactory);

            var item = new BasketItem
            {
                Quantity = 2,
                AfterDiscountPrice = 0,
                DiscountParameter = 10,
                DiscountRuleType = DiscountRuleType.None,
                Product = new Product
                {
                    Price = (decimal)20.10
                }
            };

            calculator.ApplyDiscount(item);

            Assert.AreEqual(40.20, item.AfterDiscountPrice);
        }

        [Test]
        public void GivenBasketItem_NullRule_Percentage_WhenApplyDiscount_ThenItemIsUpdatedCorrectly()
        {
            var discountFactory = new DiscountRuleFactory();
            var calculator = new BasketPriceCalculator(discountFactory);

            var item = new BasketItem
            {
                Quantity = 2,
                AfterDiscountPrice = 0,
                DiscountRuleType = null,
                DiscountParameter = 0,
                Product = new Product
                {
                    Price = (decimal)20.10,
                }
            };

            calculator.ApplyDiscount(item);

            Assert.AreEqual(40.20, item.AfterDiscountPrice);
        }
    }
}

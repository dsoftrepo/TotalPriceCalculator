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
                LastPrice = (decimal)10.50,
                Product = new Product
                {
                    DiscountRule = DiscountRuleType.BuyManyGetOneFree,
                    Price = (decimal)10.50,
                    DiscountParameter = 2
                }
            };

            var items = Enumerable.Repeat(item, 3);
            var total = calculator.CalculateTotal(items);

            Assert.AreEqual(31.50, total);
        }

        [Test]
        public void GivenBasketItem_Buy1Get1Free_WhenUpdateItemLastPrice_ThenItemIsUpdatedCorrectly()
        {
            var discountFactory = new DiscountRuleFactory();
            var calculator = new BasketPriceCalculator(discountFactory);

            var item = new BasketItem
            {
                Quantity = 2,
                LastPrice = 0,
                Product = new Product
                {
                    DiscountRule = DiscountRuleType.BuyManyGetOneFree,
                    Price = (decimal)10.50,
                    DiscountParameter = 2
                }
            };

            calculator.UpdateItemLastPrice(item);

            Assert.AreEqual(10.50, item.LastPrice);
        }

        [Test]
        public void GivenBasketItem_Buy2Get1Free_WhenUpdateItemLastPrice_ThenItemIsUpdatedCorrectly()
        {
            var discountFactory = new DiscountRuleFactory();
            var calculator = new BasketPriceCalculator(discountFactory);

            var item = new BasketItem
            {
                Quantity = 3,
                LastPrice = 0,
                Product = new Product
                {
                    DiscountRule = DiscountRuleType.BuyManyGetOneFree,
                    Price = (decimal)10.50,
                    DiscountParameter = 3
                }
            };

            calculator.UpdateItemLastPrice(item);

            Assert.AreEqual(21, item.LastPrice);
        }

        [Test]
        public void GivenBasketItem_Percentage_WhenUpdateItemLastPrice_ThenItemIsUpdatedCorrectly()
        {
            var discountFactory = new DiscountRuleFactory();
            var calculator = new BasketPriceCalculator(discountFactory);

            var item = new BasketItem
            {
                Quantity = 2,
                LastPrice = 0,
                Product = new Product
                {
                    DiscountRule = DiscountRuleType.Percentage,
                    Price = (decimal) 20.10,
                    DiscountParameter = 10
                }
            };

            calculator.UpdateItemLastPrice(item);

            Assert.AreEqual(36.18, item.LastPrice);
        }

        [Test]
        public void GivenBasketItem_NoRule_Percentage_WhenUpdateItemLastPrice_ThenItemIsUpdatedCorrectly()
        {
            var discountFactory = new DiscountRuleFactory();
            var calculator = new BasketPriceCalculator(discountFactory);

            var item = new BasketItem
            {
                Quantity = 2,
                LastPrice = 0,
                Product = new Product
                {
                    DiscountRule = DiscountRuleType.None,
                    Price = (decimal)20.10,
                    DiscountParameter = 10
                }
            };

            calculator.UpdateItemLastPrice(item);

            Assert.AreEqual(40.20, item.LastPrice);
        }

        [Test]
        public void GivenBasketItem_NullRule_Percentage_WhenUpdateItemLastPrice_ThenItemIsUpdatedCorrectly()
        {
            var discountFactory = new DiscountRuleFactory();
            var calculator = new BasketPriceCalculator(discountFactory);

            var item = new BasketItem
            {
                Quantity = 2,
                LastPrice = 0,
                Product = new Product
                {
                    DiscountRule = null,
                    Price = (decimal)20.10,
                    DiscountParameter = 0
                }
            };

            calculator.UpdateItemLastPrice(item);

            Assert.AreEqual(40.20, item.LastPrice);
        }
    }
}
